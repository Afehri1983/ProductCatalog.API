using FluentAssertions;
using Moq;
using ProductCatalog.Application.Common.Interfaces;
using ProductCatalog.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ProductCatalog.Application.Tests
{
    public class ProductServiceTests
    {
        private readonly Mock<IProductRepository> _mockRepository;
        private readonly IProductRepository _repository;

        public ProductServiceTests()
        {
            _mockRepository = new Mock<IProductRepository>();
            _repository = _mockRepository.Object;
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllProducts()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product(1, "Product 1", "Description 1", 19.99m, 5),
                new Product(2, "Product 2", "Description 2", 29.99m, 10)
            };

            _mockRepository.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(products);

            // Act
            var result = await _repository.GetAllAsync();

            // Assert
            Assert.Equal(products, result);
        }

        [Fact]
        public async Task GetByIdAsync_ExistingProduct_ReturnsProduct()
        {
            // Arrange
            var product = new Product(1, "Test Product", "Test Description", 19.99m, 5);
            _mockRepository.Setup(repo => repo.GetByIdAsync(1))
                .ReturnsAsync(product);

            // Act
            var result = await _repository.GetByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(product, result);
        }

        [Fact]
        public async Task GetByIdAsync_NonExistingProduct_ReturnsNull()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetByIdAsync(1))
                .ReturnsAsync((Product?)null);

            // Act
            var result = await _repository.GetByIdAsync(1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task AddAsync_ValidProduct_ReturnsId()
        {
            // Arrange
            var product = new Product(0, "Test Product", "Test Description", 19.99m, 5);
            var expectedId = 1;
            _mockRepository.Setup(repo => repo.AddAsync(product))
                .ReturnsAsync(expectedId);

            // Act
            var result = await _repository.AddAsync(product);

            // Assert
            Assert.Equal(expectedId, result);
        }

        [Fact]
        public async Task UpdateAsync_ExistingProduct_DoesNotThrow()
        {
            // Arrange
            var product = new Product(1, "Test Product", "Test Description", 19.99m, 5);
            _mockRepository.Setup(repo => repo.UpdateAsync(product))
                .Returns(Task.CompletedTask);

            // Act
            await _repository.UpdateAsync(product);

            // Assert
            _mockRepository.Verify(repo => repo.UpdateAsync(product), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ExistingProduct_ReturnsTrue()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.DeleteAsync(1))
                .ReturnsAsync(true);

            // Act
            var result = await _repository.DeleteAsync(1);

            // Assert
            Assert.True(result);
            _mockRepository.Verify(repo => repo.DeleteAsync(1), Times.Once);
        }
    }
}
