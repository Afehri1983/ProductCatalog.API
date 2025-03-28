using MediatR;
using ProductCatalog.Domain.Entities;

namespace ProductCatalog.Application.Products.Queries.GetProducts;

/// <summary>
/// Query to retrieve all products
/// </summary>
public record GetProductsQuery : IRequest<IEnumerable<Product>>; 