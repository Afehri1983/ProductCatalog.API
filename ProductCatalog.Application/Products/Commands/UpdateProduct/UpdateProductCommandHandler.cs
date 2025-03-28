using MediatR;
using Microsoft.Extensions.Logging;
using ProductCatalog.Domain.Entities;
using ProductCatalog.Domain.Interfaces;

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
        if (request == null)
        {
            _logger.LogWarning("UpdateProductCommand is null");
            return false;
        }

        var product = await _repository.GetByIdAsync(request.Id);
        if (product == null)
        {
            _logger.LogWarning("Product with ID {ProductId} not found", request.Id);
            return false;
        }

        if (!product.CanUpdatePrice(request.Price))
        {
            _logger.LogWarning("Price change for product {ProductId} exceeds 20% limit", request.Id);
            return false;
        }

        product.Update(request.Name, request.Description, request.Price, request.Stock);
        await _repository.UpdateAsync(product);
        return true;
    }
} 