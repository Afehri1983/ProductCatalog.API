using MediatR;
using Microsoft.Extensions.Logging;
using ProductCatalog.Application.Common.Interfaces;
using ProductCatalog.Domain.Entities;

namespace ProductCatalog.Application.Products.Commands.UpdateProduct;

/// <summary>
/// Handler for updating a product
/// </summary>
public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, bool>
{
    private readonly IProductRepository _repository;
    private readonly ILogger<UpdateProductCommandHandler> _logger;

    public UpdateProductCommandHandler(IProductRepository repository, ILogger<UpdateProductCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    /// <summary>
    /// Handles the update product command
    /// </summary>
    /// <param name="request">The update product command</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>True if the product was updated, false if the product was not found</returns>
    public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Attempting to update product with ID: {Id}", request.Id);

        var existingProduct = await _repository.GetByIdAsync(request.Id);
        if (existingProduct == null)
        {
            _logger.LogWarning("Product with ID {Id} not found", request.Id);
            return false;
        }

        _logger.LogInformation("Found product with ID: {Id}, updating properties", request.Id);

        var product = new Product(
            request.Id,
            request.Name,
            request.Description,
            request.Price,
            request.Stock
        );

        await _repository.UpdateAsync(product);
        
        _logger.LogInformation("Successfully updated product with ID: {Id}", request.Id);
        return true;
    }
} 