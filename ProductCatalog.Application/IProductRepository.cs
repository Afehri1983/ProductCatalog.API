using ProductCatalog.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductCatalog.Application.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> AddProductAsync(Product product);
        Task<IEnumerable<Product>> GetProductsAsync();
        Task<Product> UpdateProductStockAsync(int productId, int stock);
        Task<bool> DeleteProductAsync(int productId);
    }
}
