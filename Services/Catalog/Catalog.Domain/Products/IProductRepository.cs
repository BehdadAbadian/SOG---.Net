using Catalog.Domain.Categories;

namespace Catalog.Domain.Products;

public interface IProductRepository
{
    Task<List<Product>> GetAll();
    Task<Product> GetById(ProductId productId);
    Task<Product> GetByName(string name);
    Task<ProductId> Insert(Product product);
    void Delete(string name);
    Task<bool> Exists(string Title);
}
