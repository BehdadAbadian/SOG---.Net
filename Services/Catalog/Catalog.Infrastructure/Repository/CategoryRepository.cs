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
    public void Delete(CategoryId categoryId)
    {
        var category = Category.CreateForDelete(categoryId);
        _context.categories.Remove(category);
        
    }

    public async Task<bool> Exists(string Title)
    {
        return await _context.categories.AllAsync(c => c.Title == Title);
    }

    public async Task<List<Category>> GetAll()
    {
        return await _context.categories.ToListAsync();
    }

    public async Task<Category> GetById(CategoryId categoryId)
    {
        return await _context.categories.FindAsync(categoryId);
    }

    public async Task<CategoryId> Insert(Category category)
    {
        await _context.categories.AddAsync(category);
        return category.Id;
    }
}
