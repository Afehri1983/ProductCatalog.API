using MediatR;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<CreateProductCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the CreateProductCommandHandler
        /// </summary>
        /// <param name="repository">The product repository</param>
        /// <param name="logger">The logger</param>
        /// <exception cref="ArgumentNullException">Thrown when repository is null</exception>
        public CreateProductCommandHandler(IProductRepository repository, ILogger<CreateProductCommandHandler> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger;
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

            if (string.IsNullOrEmpty(request.Name))
                throw new ArgumentException("Name cannot be empty", nameof(request.Name));

            if (request.Price <= 0)
                throw new ArgumentException("Price must be greater than 0", nameof(request.Price));

            if (request.Stock < 0)
                throw new ArgumentException("Stock cannot be negative", nameof(request.Stock));

            _logger.LogInformation("Creating new product with name: {Name}", request.Name);

            var product = new Product(
                0, // Temporary ID, will be replaced by the database
                request.Name,
                request.Description,
                request.Price,
                request.Stock
            );

            var id = await _repository.AddAsync(product);
            
            _logger.LogInformation("Successfully created product with ID: {Id}", id);
            return id;
        }
    }
} 