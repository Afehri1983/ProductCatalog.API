using MediatR;

namespace ProductCatalog.Application.Products.Commands.DeleteProduct;

/// <summary>
/// Command to delete an existing product
/// </summary>
public record DeleteProductCommand(int Id) : IRequest<bool>; 