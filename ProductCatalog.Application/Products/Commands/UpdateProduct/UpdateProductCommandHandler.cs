using MediatR;
using ProductCatalog.Application.Common.Interfaces;
using ProductCatalog.Domain.Entities;

namespace ProductCatalog.Application.Products.Commands.UpdateProduct
{
    /// <summary>
    /// Handler for updating a product
    /// </summary>
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, bool>
    {
        private readonly IProductRepository _repository;

        public UpdateProductCommandHandler(IProductRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
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

            var existingProduct = await _repository.GetByIdAsync(request.Id);
            if (existingProduct == null)
                return false;

            existingProduct.Update(request.Name, request.Description, request.Price, request.Stock);
            await _repository.UpdateAsync(existingProduct);
            return true;
        }
    }
} 