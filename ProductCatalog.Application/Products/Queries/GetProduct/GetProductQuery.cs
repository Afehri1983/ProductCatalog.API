using MediatR;
using ProductCatalog.Domain.Entities;

namespace ProductCatalog.Application.Products.Queries.GetProduct;

public record GetProductQuery(int Id) : IRequest<Product?>; 