using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using ProductCatalog.Application.Products.Queries.GetProduct;
using ProductCatalog.Domain.Entities;
using ProductCatalog.Domain.Interfaces;

namespace ProductCatalog.Application.Tests.Queries;

public class GetProductQueryHandlerTests
{
    private readonly Mock<IProductRepository> _mockRepository;
    private readonly Mock<ILogger<GetProductQueryHandler>> _mockLogger;
    private readonly GetProductQueryHandler _handler;

    public GetProductQueryHandlerTests()
    {
        _mockRepository = new Mock<IProductRepository>();
        _mockLogger = new Mock<ILogger<GetProductQueryHandler>>();
        _handler = new GetProductQueryHandler(_mockRepository.Object, _mockLogger.Object);
    }

    [Fact]
    public async Task Handle_ExistingProduct_ReturnsProduct()
    {
        // Arrange
        var product = new Product(1, "Test Product", "Description", 10.99m, 5);
        var query = new GetProductQuery(1);

        _mockRepository.Setup(repo => repo.GetByIdAsync(1))
            .ReturnsAsync(product);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(product);
    }

    [Fact]
    public async Task Handle_NonExistingProduct_ReturnsNull()
    {
        // Arrange
        var query = new GetProductQuery(999);

        _mockRepository.Setup(repo => repo.GetByIdAsync(999))
            .ReturnsAsync((Product?)null);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task Handle_InvalidId_ThrowsArgumentException()
    {
        // Arrange
        var query = new GetProductQuery(-1);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => _handler.Handle(query, CancellationToken.None));
        _mockRepository.Verify(repo => repo.GetByIdAsync(It.IsAny<int>()), Times.Never);
    }
} 