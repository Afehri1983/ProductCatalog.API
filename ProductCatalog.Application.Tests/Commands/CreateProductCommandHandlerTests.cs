using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using ProductCatalog.Application.Products.Commands.CreateProduct;
using ProductCatalog.Application.Products.DTOs;
using ProductCatalog.Application.Products.Mapping;
using ProductCatalog.Domain.Entities;
using ProductCatalog.Domain.Interfaces;
using Xunit;

namespace ProductCatalog.Application.Tests.Commands
{
    public class CreateProductCommandHandlerTests
    {
        private readonly Mock<IProductRepository> _mockRepository;
        private readonly Mock<ILogger<CreateProductCommandHandler>> _mockLogger;
        private readonly IMapper _mapper;
        private readonly CreateProductCommandHandler _handler;

        public CreateProductCommandHandlerTests()
        {
            _mockRepository = new Mock<IProductRepository>();
            _mockLogger = new Mock<ILogger<CreateProductCommandHandler>>();
            
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ProductMappingProfile());
            });
            _mapper = mappingConfig.CreateMapper();
            
            _handler = new CreateProductCommandHandler(_mockRepository.Object, _mockLogger.Object, _mapper);
        }

        [Fact]
        public async Task Handle_ValidProduct_ReturnsId()
        {
            // Arrange
            var command = new CreateProductCommand("Test Product", "Description", 10.99m, 5);
            _mockRepository.Setup(repo => repo.AddAsync(It.IsAny<Product>()))
                .ReturnsAsync(1);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().Be(1);
            _mockRepository.Verify(repo => repo.AddAsync(It.Is<Product>(p =>
                p.Name == command.Name &&
                p.Description == command.Description &&
                p.Price == command.Price &&
                p.Stock == command.Stock)), Times.Once);
        }

        [Theory]
        [InlineData("", "Description", 10.99, 5)]
        [InlineData("Test Product", "", 10.99, 5)]
        [InlineData("Test Product", "Description", 0, 5)]
        [InlineData("Test Product", "Description", -1, 5)]
        [InlineData("Test Product", "Description", 10.99, -1)]
        public async Task Handle_InvalidProduct_ThrowsArgumentException(string name, string description, decimal price, int stock)
        {
            // Arrange
            var command = new CreateProductCommand(name, description, price, stock);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _handler.Handle(command, CancellationToken.None));
            _mockRepository.Verify(repo => repo.AddAsync(It.IsAny<Product>()), Times.Never);
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