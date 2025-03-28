using MediatR;
using Microsoft.Extensions.Logging;
using ProductCatalog.Domain.Entities;
using ProductCatalog.Domain.Interfaces;

namespace ProductCatalog.Application.Products.Commands.DeleteProduct;

/// <summary>
/// Handler for deleting an existing product
/// </summary>
public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
{
    private readonly IProductRepository _repository;
    private readonly ILogger<DeleteProductCommandHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the DeleteProductCommandHandler
    /// </summary>
    /// <param name="repository">The product repository</param>
    /// <param name="logger">The logger</param>
    /// <exception cref="ArgumentNullException">Thrown when repository is null</exception>
    public DeleteProductCommandHandler(IProductRepository repository, ILogger<DeleteProductCommandHandler> logger)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _logger = logger;
    }

    /// <summary>
    /// Handles the delete product command
    /// </summary>
    /// <param name="request">The delete product command</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>True if the product was deleted, false if the product was not found</returns>
    /// <exception cref="ArgumentNullException">Thrown when request is null</exception>
    public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        if (request.Id <= 0)
        {
            throw new ArgumentException("Id must be greater than 0", nameof(request.Id));
        }

        _logger.LogInformation("Attempting to delete product with ID: {ProductId}", request.Id);
        var result = await _repository.DeleteAsync(request.Id);

        if (!result)
        {
            _logger.LogWarning("Product with ID {ProductId} not found", request.Id);
        }
        else
        {
            _logger.LogInformation("Successfully deleted product with ID: {ProductId}", request.Id);
        }

        return result;
    }
} 