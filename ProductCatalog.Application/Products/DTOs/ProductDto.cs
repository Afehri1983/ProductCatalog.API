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
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The description of the product
    /// </summary>
    [Required]
    [StringLength(500)]
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// The price of the product
    /// </summary>
    [Required]
    [Range(0.01, double.MaxValue)]
    public decimal Price { get; set; }

    /// <summary>
    /// The current stock level of the product
    /// </summary>
    [Required]
    [Range(0, int.MaxValue)]
    public int Stock { get; set; }
} 