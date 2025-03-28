using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using ProductCatalog.Application.Products.Commands.UpdateProduct;
using ProductCatalog.Domain.Entities;
using ProductCatalog.Domain.Interfaces;
using Xunit;

namespace ProductCatalog.Application.Tests.Commands;

public class UpdateProductCommandHandlerTests
{
    private readonly Mock<IProductRepository> _mockRepository;
    private readonly Mock<ILogger<UpdateProductCommandHandler>> _mockLogger;
    private readonly UpdateProductCommandHandler _handler;
    private readonly Product _existingProduct;

    public UpdateProductCommandHandlerTests()
    {
        _mockRepository = new Mock<IProductRepository>();
        _mockLogger = new Mock<ILogger<UpdateProductCommandHandler>>();
        _handler = new UpdateProductCommandHandler(_mockRepository.Object, _mockLogger.Object);
        
        _existingProduct = new Product(1, "Test Product", "Test Description", 100, 10);
    }

    [Fact]
    public async Task Handle_ValidUpdate_ReturnsTrue()
    {
        // Arrange
        var command = new UpdateProductCommand(1, "Updated Name", "Updated Description", 110, 20);
        _mockRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(_existingProduct);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result);
        _mockRepository.Verify(r => r.UpdateAsync(It.IsAny<Product>()), Times.Once);
    }

    [Fact]
    public async Task Handle_PriceChangeExceeds20Percent_ReturnsFalse()
    {
        // Arrange
        var command = new UpdateProductCommand(1, "Updated Name", "Updated Description", 150, 20);
        _mockRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(_existingProduct);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result);
        _mockRepository.Verify(r => r.UpdateAsync(It.IsAny<Product>()), Times.Never);
    }

    [Fact]
    public async Task Handle_ProductNotFound_ReturnsFalse()
    {
        // Arrange
        var command = new UpdateProductCommand(999, "Updated Name", "Updated Description", 110, 20);
        _mockRepository.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((Product?)null);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result);
        _mockRepository.Verify(r => r.UpdateAsync(It.IsAny<Product>()), Times.Never);
    }

    [Fact]
    public async Task Handle_NullCommand_ReturnsFalse()
    {
        // Act
        var result = await _handler.Handle(null!, CancellationToken.None);

        // Assert
        Assert.False(result);
        _mockRepository.Verify(r => r.UpdateAsync(It.IsAny<Product>()), Times.Never);
    }
} 