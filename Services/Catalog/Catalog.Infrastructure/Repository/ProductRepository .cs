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
    public void Delete(string name)
    {
        var product = _context.products.SingleOrDefault(x => x.Name == name);
        _context.products.Remove(product);
        
    }

    public async Task<bool> Exists(string name)
    {
        return await _context.products.AnyAsync(c => c.Name == name);
    }

    public async Task<List<Product>> GetAll()
    {
        return await _context.products.AsNoTracking().ToListAsync();
    }

    public async Task<Product> GetById(ProductId productId)
    {
        return await _context.products.FindAsync(productId);
    }

    public async Task<Product> GetByName(string name)
    {
        return await _context.products.SingleOrDefaultAsync(x => x.Name == name);
    }

    public async Task<ProductId> Insert(Product product)
    {
        await _context.products.AddAsync(product);
        return product.Id;
    }
}
