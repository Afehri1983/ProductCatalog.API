using System;
using System.Threading;
using System.Threading.Tasks;
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
        private readonly UpdateProductCommandHandler _handler;

        public UpdateProductCommandHandlerTests()
        {
            _mockRepository = new Mock<IProductRepository>();
            _handler = new UpdateProductCommandHandler(_mockRepository.Object);
        }

        [Fact]
        public async Task Handle_ValidCommand_ShouldUpdateProduct()
        {
            // Arrange
            var product = new Product("Test Product", "Test Description", 100, 10);

            _mockRepository.Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(product);

            var command = new UpdateProductCommand
            {
                Id = 1,
                Name = "Updated Product",
                Description = "Updated Description",
                Price = 200,
                Stock = 20
            };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result);
            _mockRepository.Verify(r => r.GetByIdAsync(1), Times.Once);
            _mockRepository.Verify(r => r.UpdateAsync(It.IsAny<Product>()), Times.Once);
        }

        [Fact]
        public async Task Handle_NonExistentProduct_ShouldReturnFalse()
        {
            // Arrange
            _mockRepository.Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync((Product)null);

            var command = new UpdateProductCommand
            {
                Id = 1,
                Name = "Updated Product",
                Description = "Updated Description",
                Price = 200,
                Stock = 20
            };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result);
            _mockRepository.Verify(r => r.GetByIdAsync(1), Times.Once);
            _mockRepository.Verify(r => r.UpdateAsync(It.IsAny<Product>()), Times.Never);
        }

        [Fact]
        public async Task Handle_InvalidId_ShouldThrowArgumentException()
        {
            // Arrange
            var command = new UpdateProductCommand
            {
                Id = 0,
                Name = "Updated Product",
                Description = "Updated Description",
                Price = 200,
                Stock = 20
            };

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => 
                _handler.Handle(command, CancellationToken.None));
            _mockRepository.Verify(r => r.GetByIdAsync(It.IsAny<int>()), Times.Never);
            _mockRepository.Verify(r => r.UpdateAsync(It.IsAny<Product>()), Times.Never);
        }

        [Fact]
        public async Task Handle_NullCommand_ShouldThrowArgumentNullException()
        {
            // Arrange
            UpdateProductCommand? command = null;

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => 
                _handler.Handle(command!, CancellationToken.None));
            _mockRepository.Verify(r => r.GetByIdAsync(It.IsAny<int>()), Times.Never);
            _mockRepository.Verify(r => r.UpdateAsync(It.IsAny<Product>()), Times.Never);
        }
    }
} 