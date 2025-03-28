using MediatR;
using ProductCatalog.Application.Products.DTOs;

namespace ProductCatalog.Application.Products.Queries.GetProducts;

public class GetProductsQuery : IRequest<IEnumerable<ProductDto>>
{
} 