using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ProductCatalog.Application.Products.DTOs;
using ProductCatalog.Domain.Interfaces;

namespace ProductCatalog.Application.Products.Queries.GetProducts;

/// <summary>
/// Handler for retrieving all products
/// </summary>
public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, IEnumerable<ProductDto>>
{
    private readonly IProductRepository _repository;
    private readonly ILogger<GetProductsQueryHandler> _logger;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the GetProductsQueryHandler
    /// </summary>
    /// <param name="repository">The product repository</param>
    /// <param name="logger">The logger</param>
    /// <param name="mapper">The AutoMapper instance</param>
    /// <exception cref="ArgumentNullException">Thrown when repository is null</exception>
    public GetProductsQueryHandler(
        IProductRepository repository,
        ILogger<GetProductsQueryHandler> logger,
        IMapper mapper)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _logger = logger;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the get products query
    /// </summary>
    /// <param name="request">The get products query</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>A collection of products</returns>
    /// <exception cref="ArgumentNullException">Thrown when request is null</exception>
    public async Task<IEnumerable<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        if (request == null)
        {
            _logger.LogWarning("GetProductsQuery is null");
            return Enumerable.Empty<ProductDto>();
        }

        var products = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<ProductDto>>(products);
    }
} 