using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using ProductCatalog.Application.Common.Interfaces;
using ProductCatalog.Application.Products.Commands.UpdateProduct;
using ProductCatalog.Domain.Entities;
using Xunit;

namespace ProductCatalog.Application.Tests.Commands
{
    public class UpdateProductCommandHandlerTests
    {
        private readonly Mock<IProductRepository> _mockRepository;
        private readonly Mock<ILogger<UpdateProductCommandHandler>> _mockLogger;
        private readonly UpdateProductCommandHandler _handler;

        public UpdateProductCommandHandlerTests()
        {
            _mockRepository = new Mock<IProductRepository>();
            _mockLogger = new Mock<ILogger<UpdateProductCommandHandler>>();
            _handler = new UpdateProductCommandHandler(_mockRepository.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task Handle_ExistingProduct_ReturnsTrue()
        {
            // Arrange
            var existingProduct = new Product(1, "Old Name", "Old Description", 19.99m, 5);
            var command = new UpdateProductCommand(1, "New Name", "New Description", 29.99m, 10);

            _mockRepository.Setup(repo => repo.GetByIdAsync(command.Id))
                .ReturnsAsync(existingProduct);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result);
            _mockRepository.Verify(repo => repo.UpdateAsync(It.Is<Product>(p =>
                p.Id == command.Id &&
                p.Name == command.Name &&
                p.Description == command.Description &&
                p.Price == command.Price &&
                p.Stock == command.Stock)), Times.Once);
        }

        [Fact]
        public async Task Handle_NonExistingProduct_ReturnsFalse()
        {
            // Arrange
            var command = new UpdateProductCommand(1, "New Name", "New Description", 29.99m, 10);
            _mockRepository.Setup(repo => repo.GetByIdAsync(command.Id))
                .ReturnsAsync((Product?)null);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result);
            _mockRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Product>()), Times.Never);
        }

        [Fact]
        public async Task Handle_NullCommand_ThrowsArgumentNullException()
        {
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() =>
                _handler.Handle(null!, CancellationToken.None));
        }
    }
} 