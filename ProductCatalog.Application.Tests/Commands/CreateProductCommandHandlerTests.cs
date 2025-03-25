using Moq;
using ProductCatalog.Application.Common.Interfaces;
using ProductCatalog.Application.Products.Commands.CreateProduct;
using ProductCatalog.Domain.Entities;
using Xunit;
using Microsoft.Extensions.Logging;

namespace ProductCatalog.Application.Tests.Commands
{
    public class CreateProductCommandHandlerTests
    {
        private readonly Mock<IProductRepository> _mockRepository;
        private readonly Mock<ILogger<CreateProductCommandHandler>> _mockLogger;
        private readonly CreateProductCommandHandler _handler;

        public CreateProductCommandHandlerTests()
        {
            _mockRepository = new Mock<IProductRepository>();
            _mockLogger = new Mock<ILogger<CreateProductCommandHandler>>();
            _handler = new CreateProductCommandHandler(_mockRepository.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task Handle_ValidProduct_ReturnsId()
        {
            // Arrange
            var command = new CreateProductCommand("Test Product", "Test Description", 9.99m, 10);
            var expectedId = 1;
            _mockRepository.Setup(repo => repo.AddAsync(It.IsAny<Product>()))
                .ReturnsAsync(expectedId);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(expectedId, result);
            _mockRepository.Verify(repo => repo.AddAsync(It.Is<Product>(p =>
                p.Name == command.Name &&
                p.Description == command.Description &&
                p.Price == command.Price &&
                p.Stock == command.Stock)), Times.Once);
        }

        [Fact]
        public async Task Handle_InvalidPrice_ShouldThrowArgumentException()
        {
            // Arrange
            var command = new CreateProductCommand("Test Product", "Test Description", -100m, 10);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => 
                _handler.Handle(command, CancellationToken.None));
            _mockRepository.Verify(r => r.AddAsync(It.IsAny<Product>()), Times.Never);
        }

        [Fact]
        public async Task Handle_InvalidStock_ShouldThrowArgumentException()
        {
            // Arrange
            var command = new CreateProductCommand("Test Product", "Test Description", 100m, -10);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => 
                _handler.Handle(command, CancellationToken.None));
            _mockRepository.Verify(r => r.AddAsync(It.IsAny<Product>()), Times.Never);
        }

        [Fact]
        public async Task Handle_EmptyName_ShouldThrowArgumentException()
        {
            // Arrange
            var command = new CreateProductCommand(string.Empty, "Test Description", 100m, 10);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => 
                _handler.Handle(command, CancellationToken.None));
            _mockRepository.Verify(r => r.AddAsync(It.IsAny<Product>()), Times.Never);
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