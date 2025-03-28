using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ProductCatalog.Application.Products.DTOs;
using ProductCatalog.Application.Products.Mapping;
using ProductCatalog.Domain.Entities;
using ProductCatalog.Domain.Interfaces;

namespace ProductCatalog.Application.Products.Commands.CreateProduct;

/// <summary>
/// Handler for creating a new product
/// </summary>
public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
{
    private readonly IProductRepository _repository;
    private readonly ILogger<CreateProductCommandHandler> _logger;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the CreateProductCommandHandler
    /// </summary>
    /// <param name="repository">The product repository</param>
    /// <param name="logger">The logger</param>
    /// <param name="mapper">The AutoMapper instance</param>
    /// <exception cref="ArgumentNullException">Thrown when repository is null</exception>
    public CreateProductCommandHandler(
        IProductRepository repository,
        ILogger<CreateProductCommandHandler> logger,
        IMapper mapper)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _logger = logger;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the create product command
    /// </summary>
    /// <param name="command">The create product command</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>The ID of the newly created product</returns>
    /// <exception cref="ArgumentNullException">Thrown when command is null</exception>
    public async Task<int> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        if (command == null)
        {
            throw new ArgumentNullException(nameof(command));
        }

        if (string.IsNullOrWhiteSpace(command.Name))
        {
            throw new ArgumentException("Name cannot be empty", nameof(command.Name));
        }

        if (string.IsNullOrWhiteSpace(command.Description))
        {
            throw new ArgumentException("Description cannot be empty", nameof(command.Description));
        }

        if (command.Price <= 0)
        {
            throw new ArgumentException("Price must be greater than 0", nameof(command.Price));
        }

        if (command.Stock < 0)
        {
            throw new ArgumentException("Stock cannot be negative", nameof(command.Stock));
        }

        _logger.LogInformation("Creating new product with name: {Name}", command.Name);

        var productDto = new ProductDto
        {
            Name = command.Name,
            Description = command.Description,
            Price = command.Price,
            Stock = command.Stock
        };

        var product = _mapper.Map<Product>(productDto);
        var id = await _repository.AddAsync(product);
        
        _logger.LogInformation("Successfully created product with ID: {Id}", id);
        return id;
    }
} 