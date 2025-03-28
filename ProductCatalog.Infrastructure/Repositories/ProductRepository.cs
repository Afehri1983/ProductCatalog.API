using Microsoft.EntityFrameworkCore;
using ProductCatalog.Domain.Entities;
using ProductCatalog.Domain.Interfaces;
using ProductCatalog.Infrastructure.Persistence;

namespace ProductCatalog.Infrastructure.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context)
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