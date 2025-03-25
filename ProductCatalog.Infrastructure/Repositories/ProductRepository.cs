using ProductCatalog.Application.Common.Interfaces;
using ProductCatalog.Domain.Entities;

namespace ProductCatalog.Infrastructure.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(ProductCatalogDbContext context) : base(context)
        {
        }

        public override async Task UpdateAsync(Product product)
        {
            var existingProduct = await GetByIdAsync(product.Id);
            if (existingProduct == null)
            {
                throw new KeyNotFoundException($"Product with ID {product.Id} not found");
            }

            existingProduct.Update(product.Name, product.Description, product.Price, product.Stock);
            await base.UpdateAsync(existingProduct);
        }
    }
} 