using MediatR;
using ProductCatalog.Application.Common.Interfaces;
using ProductCatalog.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace ProductCatalog.Application.Products.Commands.UpdateProduct
{
    /// <summary>
    /// Handler for updating a product
    /// </summary>
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, bool>
    {
        private readonly IProductRepository _repository;
        private readonly ILogger<UpdateProductCommandHandler> _logger;

        public UpdateProductCommandHandler(IProductRepository repository, ILogger<UpdateProductCommandHandler> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the update product command
        /// </summary>
        /// <param name="request">The update product command</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>True if the product was updated, false if the product was not found</returns>
        public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            _logger.LogInformation("Attempting to update product with ID: {ProductId}", request.Id);

            var existingProduct = await _repository.GetByIdAsync(request.Id);
            if (existingProduct == null)
            {
                _logger.LogWarning("Product with ID {ProductId} not found", request.Id);
                return false;
            }

            _logger.LogInformation("Found product with ID: {ProductId}, updating properties", request.Id);

            existingProduct.Update(request.Name, request.Description, request.Price, request.Stock);
            await _repository.UpdateAsync(existingProduct);
            
            _logger.LogInformation("Successfully updated product with ID: {ProductId}", request.Id);
            return true;
        }
    }
} 