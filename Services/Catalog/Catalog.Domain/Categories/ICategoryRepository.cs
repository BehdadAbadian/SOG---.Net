namespace Catalog.Domain.Categories;

public interface ICategoryRepository
{
    Task<List<Category>> GetAll();
    Task<Category> GetById(CategoryId categoryId);
    Task<CategoryId> Insert(Category category);
    void Delete(CategoryId categoryId);
    Task<bool> Exists(string Title);
}
