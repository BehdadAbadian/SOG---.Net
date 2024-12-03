using Catalog.Domain.Base;

namespace Catalog.Domain.Categories;

public class Category : AggregateRoot<CategoryId>
{
    public string Title { get; private set; }
    public string Description { get; private set; }
    public bool IsActive { get; private set; }
    

    public static Category CreateNew(string title, string description)
    {
        var categoryId = new CategoryId(Guid.NewGuid());
        return new Category(title, description, categoryId);
    }
    public static Category CreateForDelete(CategoryId categoryId)
    {
        return new Category(categoryId);
    }
    private Category() { }
    private Category(CategoryId id)
    {
        Id = id;
    }

    private Category(string title, string description, CategoryId id)
    {
        Id= id;
        Title = title;
        Description = description;
        IsActive = true;
    }
}
