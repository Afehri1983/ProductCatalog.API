using System;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using ProductCatalog.Application.Common.Interfaces;
using ProductCatalog.Application.Products.Commands.CreateProduct;
using ProductCatalog.Domain.Entities;
using Xunit;

namespace ProductCatalog.Application.Tests.Commands
{
    public class CreateProductCommandHandlerTests
    {
        private readonly Mock<IProductRepository> _mockRepository;
        private readonly CreateProductCommandHandler _handler;

        public CreateProductCommandHandlerTests()
        {
            _mockRepository = new Mock<IProductRepository>();
            _handler = new CreateProductCommandHandler(_mockRepository.Object);
        }

        [Fact]
        public async Task Handle_ValidCommand_ShouldCreateProduct()
        {
            // Arrange
            var command = new CreateProductCommand
            {
                Name = "Test Product",
                Description = "Test Description",
                Price = 100,
                Stock = 10
            };

            _mockRepository.Setup(r => r.AddAsync(It.IsAny<Product>()))
                .ReturnsAsync(1);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(1, result);
            _mockRepository.Verify(r => r.AddAsync(It.IsAny<Product>()), Times.Once);
        }

        [Fact]
        public async Task Handle_InvalidPrice_ShouldThrowArgumentException()
        {
            // Arrange
            var command = new CreateProductCommand
            {
                Name = "Test Product",
                Description = "Test Description",
                Price = -100,
                Stock = 10
            };

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => 
                _handler.Handle(command, CancellationToken.None));
            _mockRepository.Verify(r => r.AddAsync(It.IsAny<Product>()), Times.Never);
        }

        [Fact]
        public async Task Handle_InvalidStock_ShouldThrowArgumentException()
        {
            // Arrange
            var command = new CreateProductCommand
            {
                Name = "Test Product",
                Description = "Test Description",
                Price = 100,
                Stock = -10
            };

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => 
                _handler.Handle(command, CancellationToken.None));
            _mockRepository.Verify(r => r.AddAsync(It.IsAny<Product>()), Times.Never);
        }

        [Fact]
        public async Task Handle_EmptyName_ShouldThrowArgumentException()
        {
            // Arrange
            var command = new CreateProductCommand
            {
                Name = string.Empty,
                Description = "Test Description",
                Price = 100,
                Stock = 10
            };

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => 
                _handler.Handle(command, CancellationToken.None));
            _mockRepository.Verify(r => r.AddAsync(It.IsAny<Product>()), Times.Never);
        }

        [Fact]
        public async Task Handle_NullCommand_ShouldThrowArgumentNullException()
        {
            // Arrange
            CreateProductCommand? command = null;

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => 
                _handler.Handle(command!, CancellationToken.None));
            _mockRepository.Verify(r => r.AddAsync(It.IsAny<Product>()), Times.Never);
        }
    }
} 