using ProductCatalog.Domain.Entities;

namespace ProductCatalog.Application.Common.Interfaces
{
    public interface IProductService
    {
        Task<Product> CreateProductAsync(Product product);
        Task<IEnumerable<Product>> GetProductsAsync();
        Task<Product?> GetProductAsync(int id);
        Task<Product?> UpdateProductStockAsync(int id, int stock);
        Task<bool> DeleteProductAsync(int id);
    }
} 