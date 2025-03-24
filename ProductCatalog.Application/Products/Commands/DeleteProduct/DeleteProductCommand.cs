using MediatR;

namespace ProductCatalog.Application.Products.Commands.DeleteProduct
{
    public record DeleteProductCommand : IRequest<bool>
    {
        public int Id { get; init; }
    }
} 