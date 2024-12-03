using Catalog.Domain.Base;
using Catalog.Domain.Categories;

namespace Catalog.Domain.Products;

public class Product : AggregateRoot<ProductId>
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public double Code { get; private set; }
    public double Price { get; private set; }
    public readonly CategoryId CategoryId;

    public static Product CreateNew(string name, string description, double code, double price, CategoryId categoryId)
    {
        var productId = new ProductId(Guid.NewGuid());
        return new Product(productId, name, description, code, price, categoryId);
    }
    public static Product CreateForDelete(ProductId productId)
    {
        return new Product(productId);
    }
    
    private Product() { }
    public Product(ProductId productId)
    {
        Id = productId;
    }

    private Product(ProductId id, string name, string description, double code, double price, CategoryId categoryId)
    {
        if (Price < 0)
            throw new BusinessRuleException("The Price Cant be Negative");
        Id = id;
        Name = name;
        Description = description;
        Code = code;
        Price = price;
        CategoryId = categoryId;
    }
}
