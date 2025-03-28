using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using ProductCatalog.Application.Products.Commands.DeleteProduct;
using ProductCatalog.Domain.Entities;
using ProductCatalog.Domain.Interfaces;

namespace ProductCatalog.Application.Tests.Commands;

public class DeleteProductCommandHandlerTests
{
    private readonly Mock<IProductRepository> _mockRepository;
    private readonly Mock<ILogger<DeleteProductCommandHandler>> _mockLogger;
    private readonly DeleteProductCommandHandler _handler;

    public DeleteProductCommandHandlerTests()
    {
        _mockRepository = new Mock<IProductRepository>();
        _mockLogger = new Mock<ILogger<DeleteProductCommandHandler>>();
        _handler = new DeleteProductCommandHandler(_mockRepository.Object, _mockLogger.Object);
    }

    [Fact]
    public async Task Handle_ExistingProduct_ReturnsTrue()
    {
        // Arrange
        var command = new DeleteProductCommand(1);
        _mockRepository.Setup(repo => repo.DeleteAsync(1))
            .ReturnsAsync(true);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().BeTrue();
        _mockRepository.Verify(repo => repo.DeleteAsync(1), Times.Once);
    }

    [Fact]
    public async Task Handle_NonExistingProduct_ReturnsFalse()
    {
        // Arrange
        var command = new DeleteProductCommand(999);
        _mockRepository.Setup(repo => repo.DeleteAsync(999))
            .ReturnsAsync(false);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().BeFalse();
        _mockRepository.Verify(repo => repo.DeleteAsync(999), Times.Once);
    }

    [Fact]
    public async Task Handle_InvalidId_ThrowsArgumentException()
    {
        // Arrange
        var command = new DeleteProductCommand(-1);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => _handler.Handle(command, CancellationToken.None));
        _mockRepository.Verify(repo => repo.DeleteAsync(It.IsAny<int>()), Times.Never);
    }
} 