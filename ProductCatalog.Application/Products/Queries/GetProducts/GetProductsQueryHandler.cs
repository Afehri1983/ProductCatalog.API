using MediatR;
using ProductCatalog.Application.Common.Interfaces;
using ProductCatalog.Application.Products.DTOs;

namespace ProductCatalog.Application.Products.Queries.GetProducts
{
    /// <summary>
    /// Handler for retrieving all products
    /// </summary>
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, IEnumerable<ProductDto>>
    {
        private readonly IProductRepository _repository;

        /// <summary>
        /// Initializes a new instance of the GetProductsQueryHandler
        /// </summary>
        /// <param name="repository">The product repository</param>
        /// <exception cref="ArgumentNullException">Thrown when repository is null</exception>
        public GetProductsQueryHandler(IProductRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        /// <summary>
        /// Handles the get products query
        /// </summary>
        /// <param name="request">The get products query</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>A collection of product DTOs</returns>
        /// <exception cref="ArgumentNullException">Thrown when request is null</exception>
        public async Task<IEnumerable<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var products = await _repository.GetAllAsync();
            
            return products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Stock = p.Stock
            });
        }
    }
} 