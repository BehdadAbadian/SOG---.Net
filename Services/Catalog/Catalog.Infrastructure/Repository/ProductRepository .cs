using Catalog.Domain.Categories;
using Catalog.Domain.Products;
using Catalog.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infrastructure.Repository;

public class ProductRepository : IProductRepository
{
    private readonly CatalogContext _context;

    public ProductRepository(CatalogContext context)
    {
        _context = context;
    }
    public void Delete(ProductId productId)
    {
        var product = Product.CreateForDelete(productId);
        _context.products.Remove(product);
        
    }

    public async Task<bool> Exists(string name)
    {
        return await _context.products.AllAsync(c => c.Name == name);
    }

    public async Task<List<Product>> GetAll()
    {
        return await _context.products.ToListAsync();
    }

    public async Task<Product> GetById(ProductId productId)
    {
        return await _context.products.FindAsync(productId);
    }

    public async Task<ProductId> Insert(Product product)
    {
        await _context.products.AddAsync(product);
        return product.Id;
    }
}
