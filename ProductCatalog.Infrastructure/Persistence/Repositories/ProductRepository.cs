using Microsoft.EntityFrameworkCore;
using ProductCatalog.Application.Common.Interfaces;
using ProductCatalog.Domain.Entities;

namespace ProductCatalog.Infrastructure.Persistence.Repositories;

/// <summary>
/// Repository for managing Product entities in the database
/// </summary>
public class ProductRepository : IProductRepository
{
    private readonly ProductCatalogDbContext _context;

    public ProductRepository(ProductCatalogDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Retrieves all products from the database
    /// </summary>
    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _context.Products.ToListAsync();
    }

    /// <summary>
    /// Retrieves a product by its ID
    /// </summary>
    /// <param name="id">The ID of the product to retrieve</param>
    /// <returns>The product if found, null otherwise</returns>
    public async Task<Product?> GetByIdAsync(int id)
    {
        return await _context.Products.FindAsync(id);
    }

    /// <summary>
    /// Adds a new product to the database
    /// </summary>
    /// <param name="product">The product to add</param>
    /// <returns>The ID of the newly created product</returns>
    public async Task<int> AddAsync(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return product.Id;
    }

    /// <summary>
    /// Updates an existing product in the database
    /// </summary>
    /// <param name="product">The product to update</param>
    /// <exception cref="KeyNotFoundException">Thrown when the product is not found</exception>
    public async Task UpdateAsync(Product product)
    {
        var existingProduct = await _context.Products.FindAsync(product.Id);
        if (existingProduct == null)
        {
            throw new KeyNotFoundException($"Product with ID {product.Id} not found");
        }

        _context.Entry(existingProduct).CurrentValues.SetValues(product);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Deletes a product from the database
    /// </summary>
    /// <param name="id">The ID of the product to delete</param>
    public async Task<bool> DeleteAsync(int id)
    {
        var product = await GetByIdAsync(id);
        if (product == null)
        {
            return false;
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        return true;
    }
} 