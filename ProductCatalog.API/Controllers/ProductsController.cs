using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Application.Products.Commands.CreateProduct;
using ProductCatalog.Application.Products.Commands.DeleteProduct;
using ProductCatalog.Application.Products.Commands.UpdateProduct;
using ProductCatalog.Application.Products.Queries.GetProduct;
using ProductCatalog.Application.Products.Queries.GetProducts;
using ProductCatalog.Application.Products.DTOs;
using ProductCatalog.Domain.Entities;

namespace ProductCatalog.API.Controllers
{
    /// <summary>
    /// Controller for managing products
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Gets all products
        /// </summary>
        /// <returns>A list of all products</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProductDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAll()
        {
            var products = await _mediator.Send(new GetProductsQuery());
            return Ok(products);
        }

        /// <summary>
        /// Gets a product by ID
        /// </summary>
        /// <param name="id">The ID of the product</param>
        /// <returns>The product with the specified ID</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var query = new GetProductQuery(id);
            var product = await _mediator.Send(query);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        /// <summary>
        /// Creates a new product
        /// </summary>
        /// <param name="command">The product creation command</param>
        /// <returns>The ID of the newly created product</returns>
        [HttpPost]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<int>> CreateProduct(CreateProductCommand command)
        {
            try
            {
                var productId = await _mediator.Send(command);
                return CreatedAtAction(nameof(GetProduct), new { id = productId }, productId);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Updates an existing product
        /// </summary>
        /// <param name="id">The ID of the product to update</param>
        /// <param name="command">The product update command</param>
        /// <returns>No content if successful</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateProduct(int id, UpdateProductCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest("The ID in the URL must match the ID in the request body");
            }

            try
            {
                var success = await _mediator.Send(command);

                if (!success)
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Deletes a product
        /// </summary>
        /// <param name="id">The ID of the product to delete</param>
        /// <returns>No content if successful</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            try
            {
                var command = new DeleteProductCommand(id);
                var success = await _mediator.Send(command);

                if (!success)
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
} 