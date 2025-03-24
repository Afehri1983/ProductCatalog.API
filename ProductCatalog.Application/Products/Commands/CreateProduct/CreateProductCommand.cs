using MediatR;

namespace ProductCatalog.Application.Products.Commands.CreateProduct
{
    public record CreateProductCommand : IRequest<int>
    {
        public string Name { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public decimal Price { get; init; }
        public int Stock { get; init; }
    }
} 