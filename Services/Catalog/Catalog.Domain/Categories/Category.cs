using Catalog.Domain.Base;

namespace Catalog.Domain.Categories;

public class Category : AggregateRoot<CategoryId>
{
    public string Title { get; private set; }
    public string Description { get; private set; }
    public bool IsActive { get; private set; }
    

    internal Category CreateNew(string title, string description)
    {
        return new Category(title, description);
    }
    private Category() { }

    private Category(string title, string description)
    {
        Title = title;
        Description = description;
        IsActive = true;
    }
}
