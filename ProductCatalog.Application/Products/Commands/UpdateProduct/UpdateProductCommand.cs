using MediatR;
using System.ComponentModel.DataAnnotations;

namespace ProductCatalog.Application.Products.Commands.UpdateProduct
{
    /// <summary>
    /// Command to update an existing product
    /// </summary>
    public record UpdateProductCommand : IRequest<bool>
    {
        /// <summary>
        /// The unique identifier of the product to update
        /// </summary>
        [Required(ErrorMessage = "Id is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Id must be greater than 0")]
        public int Id { get; init; }

        /// <summary>
        /// The new name of the product
        /// </summary>
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public string Name { get; init; } = string.Empty;

        /// <summary>
        /// The new description of the product
        /// </summary>
        [Required(ErrorMessage = "Description is required")]
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string Description { get; init; } = string.Empty;

        /// <summary>
        /// The new price of the product
        /// </summary>
        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; init; }

        /// <summary>
        /// The new stock level of the product
        /// </summary>
        [Required(ErrorMessage = "Stock is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Stock cannot be negative")]
        public int Stock { get; init; }
    }
} 