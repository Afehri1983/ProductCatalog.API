using MediatR;
using ProductCatalog.Application.Products.DTOs;

namespace ProductCatalog.Application.Products.Queries.GetProducts
{
    /// <summary>
    /// Query to retrieve all products
    /// </summary>
    public record GetProductsQuery : IRequest<IEnumerable<ProductDto>>;
} 