using MediatR;
using ProductCatalog.Application.Products.DTOs;

namespace ProductCatalog.Application.Products.Queries.GetProducts
{
    public record GetProductsQuery : IRequest<IEnumerable<ProductDto>>;
} 