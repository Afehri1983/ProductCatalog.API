using MediatR;
using ProductCatalog.Application.Common.Interfaces;

namespace ProductCatalog.Application.Products.Commands.DeleteProduct
{
    /// <summary>
    /// Handler for deleting an existing product
    /// </summary>
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
    {
        private readonly IProductRepository _repository;

        /// <summary>
        /// Initializes a new instance of the DeleteProductCommandHandler
        /// </summary>
        /// <param name="repository">The product repository</param>
        /// <exception cref="ArgumentNullException">Thrown when repository is null</exception>
        public DeleteProductCommandHandler(IProductRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        /// <summary>
        /// Handles the delete product command
        /// </summary>
        /// <param name="request">The delete product command</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>True if the product was deleted, false if the product was not found</returns>
        /// <exception cref="ArgumentNullException">Thrown when request is null</exception>
        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (request.Id <= 0)
                throw new ArgumentException("Id must be greater than 0", nameof(request.Id));

            var product = await _repository.GetByIdAsync(request.Id);
            if (product == null)
                return false;

            await _repository.DeleteAsync(request.Id);
            return true;
        }
    }
} 