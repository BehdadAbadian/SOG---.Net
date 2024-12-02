
using Catalog.Infrastructure.Database;

namespace Catalog.Infrastructure.Patterns;

public class UnitOfWork : IUnitOfWork
{
    private readonly CatalogContext _context;

    public UnitOfWork(CatalogContext context)
    {
        _context = context;
    }
    public void Dispose()
    {
        _context.Dispose();
    }

    public async Task SaveChanges()
    {
        await _context.SaveChangesAsync();
    }
}
