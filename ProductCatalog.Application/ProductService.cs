using ProductCatalog.Application.Interfaces;
using ProductCatalog.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductCatalog.Application.Services
{
    public class ProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            return await _productRepository.AddProductAsync(product);
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _productRepository.GetProductsAsync();
        }

        public async Task<Product> UpdateProductStockAsync(int productId, int stock)
        {
            return await _productRepository.UpdateProductStockAsync(productId, stock);
        }

        public async Task<bool> DeleteProductAsync(int productId)
        {
            return await _productRepository.DeleteProductAsync(productId);
        }
    }
}
