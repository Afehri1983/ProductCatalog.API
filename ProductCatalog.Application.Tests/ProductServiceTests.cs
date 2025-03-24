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
        private readonly Mock<IProductRepository> _productRepositoryMock;

        public ProductServiceTests()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnProduct_WhenProductExists()
        {
            // Arrange
            var product = new Product("Test Product", "Test Description", 10.0m, 100);
            _productRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(product);

            // Act
            var result = await _productRepositoryMock.Object.GetByIdAsync(1);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(product);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnListOfProducts()
        {
            // Arrange
            var products = new List<Product> { new Product("Test Product", "Test Description", 10.0m, 100) };
            _productRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(products);

            // Act
            var result = await _productRepositoryMock.Object.GetAllAsync();

            // Assert
            result.Should().BeEquivalentTo(products);
        }

        [Fact]
        public async Task AddAsync_ShouldReturnProductId()
        {
            // Arrange
            var product = new Product("Test Product", "Test Description", 10.0m, 100);
            _productRepositoryMock.Setup(repo => repo.AddAsync(product)).ReturnsAsync(1);

            // Act
            var result = await _productRepositoryMock.Object.AddAsync(product);

            // Assert
            result.Should().Be(1);
        }

        [Fact]
        public async Task UpdateAsync_ShouldNotThrowException()
        {
            // Arrange
            var product = new Product("Test Product", "Test Description", 10.0m, 100);
            _productRepositoryMock.Setup(repo => repo.UpdateAsync(product)).Returns(Task.CompletedTask);

            // Act
            var action = async () => await _productRepositoryMock.Object.UpdateAsync(product);

            // Assert
            await action.Should().NotThrowAsync();
        }

        [Fact]
        public async Task DeleteAsync_ShouldNotThrowException()
        {
            // Arrange
            _productRepositoryMock.Setup(repo => repo.DeleteAsync(1)).Returns(Task.CompletedTask);

            // Act
            var action = async () => await _productRepositoryMock.Object.DeleteAsync(1);

            // Assert
            await action.Should().NotThrowAsync();
        }
    }
}
