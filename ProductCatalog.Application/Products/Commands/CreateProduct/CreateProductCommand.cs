using MediatR;
using System.ComponentModel.DataAnnotations;

namespace ProductCatalog.Application.Products.Commands.CreateProduct
{
    /// <summary>
    /// Command to create a new product
    /// </summary>
    public record CreateProductCommand : IRequest<int>
    {
        /// <summary>
        /// The name of the product
        /// </summary>
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public string Name { get; init; } = string.Empty;

        /// <summary>
        /// The description of the product
        /// </summary>
        [Required(ErrorMessage = "Description is required")]
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string Description { get; init; } = string.Empty;

        /// <summary>
        /// The price of the product
        /// </summary>
        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; init; }

        /// <summary>
        /// The initial stock level of the product
        /// </summary>
        [Required(ErrorMessage = "Stock is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Stock cannot be negative")]
        public int Stock { get; init; }
    }
} 