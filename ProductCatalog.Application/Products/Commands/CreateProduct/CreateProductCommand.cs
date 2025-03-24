using MediatR;
using System.ComponentModel.DataAnnotations;
using ProductCatalog.Domain.Constants;

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
        [StringLength(ProductConstants.NameMaxLength, ErrorMessage = "Name cannot exceed {1} characters")]
        public string Name { get; init; } = string.Empty;

        /// <summary>
        /// The description of the product
        /// </summary>
        [Required(ErrorMessage = "Description is required")]
        [StringLength(ProductConstants.DescriptionMaxLength, ErrorMessage = "Description cannot exceed {1} characters")]
        public string Description { get; init; } = string.Empty;

        /// <summary>
        /// The price of the product
        /// </summary>
        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than {1}")]
        public decimal Price { get; init; }

        /// <summary>
        /// The initial stock level of the product
        /// </summary>
        [Required(ErrorMessage = "Stock is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Stock cannot be negative")]
        public int Stock { get; init; }

        public CreateProductCommand(string name, string description, decimal price, int stock)
        {
            Name = name;
            Description = description;
            Price = price;
            Stock = stock;
        }
    }
} 