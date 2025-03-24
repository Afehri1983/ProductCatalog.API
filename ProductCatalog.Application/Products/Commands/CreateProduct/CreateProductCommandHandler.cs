using MediatR;
using ProductCatalog.Application.Common.Interfaces;
using ProductCatalog.Domain.Entities;

namespace ProductCatalog.Application.Products.Commands.CreateProduct
{
    /// <summary>
    /// Handler for creating a new product
    /// </summary>
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
    {
        private readonly IProductRepository _repository;

        /// <summary>
        /// Initializes a new instance of the CreateProductCommandHandler
        /// </summary>
        /// <param name="repository">The product repository</param>
        /// <exception cref="ArgumentNullException">Thrown when repository is null</exception>
        public CreateProductCommandHandler(IProductRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        /// <summary>
        /// Handles the create product command
        /// </summary>
        /// <param name="request">The create product command</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The ID of the newly created product</returns>
        /// <exception cref="ArgumentNullException">Thrown when request is null</exception>
        public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var entity = new Product(
                request.Name,
                request.Description,
                request.Price,
                request.Stock
            );

            return await _repository.AddAsync(entity);
        }
    }
} 