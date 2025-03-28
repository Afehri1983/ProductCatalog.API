using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using ProductCatalog.Application.Products.DTOs;
using ProductCatalog.Application.Products.Mapping;
using ProductCatalog.Application.Products.Queries.GetProducts;
using ProductCatalog.Domain.Entities;
using ProductCatalog.Domain.Interfaces;
using Xunit;

namespace ProductCatalog.Application.Tests.Queries;

public class GetProductsQueryHandlerTests
{
    private readonly Mock<IProductRepository> _mockRepository;
    private readonly Mock<ILogger<GetProductsQueryHandler>> _mockLogger;
    private readonly IMapper _mapper;
    private readonly GetProductsQueryHandler _handler;

    public GetProductsQueryHandlerTests()
    {
        _mockRepository = new Mock<IProductRepository>();
        _mockLogger = new Mock<ILogger<GetProductsQueryHandler>>();
        
        var mappingConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new ProductMappingProfile());
        });
        _mapper = mappingConfig.CreateMapper();
        
        _handler = new GetProductsQueryHandler(_mockRepository.Object, _mockLogger.Object, _mapper);
    }

    [Fact]
    public async Task Handle_ReturnsAllProducts()
    {
        // Arrange
        var products = new List<Product>
        {
            new Product(1, "Test Product 1", "Description 1", 10.99m, 5),
            new Product(2, "Test Product 2", "Description 2", 20.99m, 10)
        };

        _mockRepository.Setup(repo => repo.GetAllAsync())
            .ReturnsAsync(products);

        var query = new GetProductsQuery();

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(_mapper.Map<IEnumerable<ProductDto>>(products));
    }

    [Fact]
    public async Task Handle_NoProducts_ReturnsEmptyList()
    {
        // Arrange
        _mockRepository.Setup(repo => repo.GetAllAsync())
            .ReturnsAsync(new List<Product>());

        var query = new GetProductsQuery();

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEmpty();
    }
} 