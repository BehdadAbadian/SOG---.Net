namespace Catalog.Domain.Categories;

public interface ICategoryRepository
{
    Task<List<Category>> GetAll();
    Task<Category> GetById(CategoryId categoryId);
    Task<Category> GetByName(string title);
    Task<CategoryId> Insert(Category category);
    void Delete(string Title);
    Task<bool> Exists(string Title);
}
