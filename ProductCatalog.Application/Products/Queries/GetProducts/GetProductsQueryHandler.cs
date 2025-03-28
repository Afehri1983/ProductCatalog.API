using MediatR;
using Microsoft.Extensions.Logging;
using ProductCatalog.Domain.Entities;
using ProductCatalog.Domain.Interfaces;

namespace ProductCatalog.Application.Products.Queries.GetProducts;

/// <summary>
/// Handler for retrieving all products
/// </summary>
public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, IEnumerable<Product>>
{
    private readonly IProductRepository _repository;
    private readonly ILogger<GetProductsQueryHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the GetProductsQueryHandler
    /// </summary>
    /// <param name="repository">The product repository</param>
    /// <param name="logger">The logger</param>
    /// <exception cref="ArgumentNullException">Thrown when repository is null</exception>
    public GetProductsQueryHandler(IProductRepository repository, ILogger<GetProductsQueryHandler> logger)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _logger = logger;
    }

    /// <summary>
    /// Handles the get products query
    /// </summary>
    /// <param name="request">The get products query</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>A collection of products</returns>
    /// <exception cref="ArgumentNullException">Thrown when request is null</exception>
    public async Task<IEnumerable<Product>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        if (request == null)
        {
            _logger.LogWarning("GetProductsQuery is null");
            return Enumerable.Empty<Product>();
        }

        return await _repository.GetAllAsync();
    }
} 