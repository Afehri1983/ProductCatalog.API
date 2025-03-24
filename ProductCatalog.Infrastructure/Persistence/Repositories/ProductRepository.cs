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
    public IEnumerable<Product> GetAll()
    {
        return _context.Products.ToList();
    }

    /// <summary>
    /// Retrieves a product by its ID
    /// </summary>
    /// <param name="id">The ID of the product to retrieve</param>
    /// <returns>The product if found, null otherwise</returns>
    public Product? GetById(int id)
    {
        return _context.Products.Find(id);
    }

    /// <summary>
    /// Adds a new product to the database
    /// </summary>
    /// <param name="product">The product to add</param>
    /// <returns>The ID of the newly created product</returns>
    public int Add(Product product)
    {
        _context.Products.Add(product);
        _context.SaveChanges();
        return product.Id;
    }

    /// <summary>
    /// Updates an existing product in the database
    /// </summary>
    /// <param name="product">The product to update</param>
    /// <exception cref="KeyNotFoundException">Thrown when the product is not found</exception>
    public void Update(Product product)
    {
        var existingProduct = _context.Products.Find(product.Id);
        if (existingProduct == null)
        {
            throw new KeyNotFoundException($"Product with ID {product.Id} not found");
        }

        _context.Entry(existingProduct).CurrentValues.SetValues(product);
        _context.SaveChanges();
    }

    /// <summary>
    /// Deletes a product from the database
    /// </summary>
    /// <param name="id">The ID of the product to delete</param>
    public bool Delete(int id)
    {
        var product = _context.Products.Find(id);
        if (product == null)
        {
            return false;
        }

        _context.Products.Remove(product);
        _context.SaveChanges();
        return true;
    }
} 