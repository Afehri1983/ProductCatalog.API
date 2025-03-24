using System;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using ProductCatalog.Application.Common.Interfaces;
using ProductCatalog.Application.Products.Commands.DeleteProduct;
using ProductCatalog.Domain.Entities;
using Xunit;

namespace ProductCatalog.Application.Tests.Commands
{
    public class DeleteProductCommandHandlerTests
    {
        private readonly Mock<IProductRepository> _mockRepository;
        private readonly DeleteProductCommandHandler _handler;

        public DeleteProductCommandHandlerTests()
        {
            _mockRepository = new Mock<IProductRepository>();
            _handler = new DeleteProductCommandHandler(_mockRepository.Object);
        }

        [Fact]
        public async Task Handle_ValidCommand_ShouldDeleteProduct()
        {
            // Arrange
            var command = new DeleteProductCommand { Id = 1 };

            _mockRepository.Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(new Product(1, "Test", "Test", 10m, 10));
            _mockRepository.Setup(r => r.DeleteAsync(1))
                .ReturnsAsync(true);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result);
            _mockRepository.Verify(r => r.DeleteAsync(1), Times.Once);
        }

        [Fact]
        public async Task Handle_InvalidId_ShouldThrowArgumentException()
        {
            // Arrange
            var command = new DeleteProductCommand { Id = 0 };

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => 
                _handler.Handle(command, CancellationToken.None));
            _mockRepository.Verify(r => r.DeleteAsync(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async Task Handle_NullCommand_ShouldThrowArgumentNullException()
        {
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => 
                _handler.Handle(null!, CancellationToken.None));
            _mockRepository.Verify(r => r.DeleteAsync(It.IsAny<int>()), Times.Never);
        }
    }
} 