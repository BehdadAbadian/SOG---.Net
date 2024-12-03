using Catalog.Domain.Categories;
using Catalog.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infrastructure.Repository;

public class CategoryRepository : ICategoryRepository
{
    private readonly CatalogContext _context;

    public CategoryRepository(CatalogContext context)
    {
        _context = context;
    }
    public void Delete(string Title)
    {
        var category = _context.categories.SingleOrDefault(x => x.Title == Title);
        _context.categories.Remove(category);
        
    }

    public async Task<bool> Exists(string Title)
    {
        return await _context.categories.AnyAsync(c => c.Title == Title);
    }

    public async Task<List<Category>> GetAll()
    {
        return await _context.categories.AsNoTracking().ToListAsync();
    }

    public async Task<Category> GetById(CategoryId categoryId)
    {
        return await _context.categories.FindAsync(categoryId);
    }

    public Task<Category> GetByName(string title)
    {
        return _context.categories.SingleOrDefaultAsync(x=> x.Title == title);
    }

    public async Task<CategoryId> Insert(Category category)
    {
        await _context.categories.AddAsync(category);
        return category.Id;
    }
}
