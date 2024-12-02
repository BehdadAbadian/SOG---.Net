namespace Catalog.Domain.Products;

public interface IProductRepository
{
    Task<List<Product>> GetAll();
    Task<Product> GetById(ProductId productId);
    Task<ProductId> Insert(Product product);
    void Delete(ProductId productId);
    Task<bool> Exists(string Title);
}
