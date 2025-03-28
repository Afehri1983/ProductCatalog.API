using MediatR;
using Microsoft.Extensions.Logging;
using ProductCatalog.Domain.Entities;
using ProductCatalog.Domain.Interfaces;

namespace ProductCatalog.Application.Products.Queries.GetProduct;

public class GetProductQueryHandler : IRequestHandler<GetProductQuery, Product?>
{
    private readonly IProductRepository _repository;
    private readonly ILogger<GetProductQueryHandler> _logger;

    public GetProductQueryHandler(IProductRepository repository, ILogger<GetProductQueryHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Product?> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        if (request.Id <= 0)
        {
            throw new ArgumentException("Product ID must be greater than 0", nameof(request.Id));
        }

        var product = await _repository.GetByIdAsync(request.Id);
        
        if (product == null)
        {
            _logger.LogInformation("Product with ID {ProductId} not found", request.Id);
        }

        return product;
    }
} 