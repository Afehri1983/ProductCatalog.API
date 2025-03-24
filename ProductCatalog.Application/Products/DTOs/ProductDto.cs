using System.ComponentModel.DataAnnotations;
using ProductCatalog.Domain.Constants;

namespace ProductCatalog.Application.Products.DTOs;

/// <summary>
/// Data Transfer Object for Product entity
/// </summary>
public class ProductDto
{
    /// <summary>
    /// The unique identifier of the product
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The name of the product
    /// </summary>
    [Required(ErrorMessage = "Name is required")]
    [StringLength(ProductConstants.NameMaxLength, ErrorMessage = "Name cannot exceed {1} characters")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The description of the product
    /// </summary>
    [Required(ErrorMessage = "Description is required")]
    [StringLength(ProductConstants.DescriptionMaxLength, ErrorMessage = "Description cannot exceed {1} characters")]
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// The price of the product
    /// </summary>
    [Required(ErrorMessage = "Price is required")]
    [Range(typeof(decimal), nameof(ProductConstants.MinimumPrice), "79228162514264337593543950335", ErrorMessage = "Price must be greater than {1}")]
    public decimal Price { get; set; }

    /// <summary>
    /// The current stock level of the product
    /// </summary>
    [Required(ErrorMessage = "Stock is required")]
    [Range(ProductConstants.MinimumStock, int.MaxValue, ErrorMessage = "Stock cannot be negative")]
    public int Stock { get; set; }
} 