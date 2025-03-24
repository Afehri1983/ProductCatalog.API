using MediatR;

namespace ProductCatalog.Application.Products.Commands.UpdateProduct;

/// <summary>
/// Command to update an existing product
/// </summary>
public record UpdateProductCommand(int Id, string Name, string Description, decimal Price, int Stock) : IRequest<bool>; 