namespace Catalog.Infrastructure.Patterns;

public interface IUnitOfWork
{
    Task SaveChanges();
    void Dispose();
}
