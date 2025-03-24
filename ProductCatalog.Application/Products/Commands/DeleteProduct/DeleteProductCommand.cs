using MediatR;
using System.ComponentModel.DataAnnotations;

namespace ProductCatalog.Application.Products.Commands.DeleteProduct
{
    /// <summary>
    /// Command to delete an existing product
    /// </summary>
    public record DeleteProductCommand : IRequest<bool>
    {
        /// <summary>
        /// The unique identifier of the product to delete
        /// </summary>
        [Required(ErrorMessage = "Id is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Id must be greater than 0")]
        public int Id { get; init; }
    }
} 