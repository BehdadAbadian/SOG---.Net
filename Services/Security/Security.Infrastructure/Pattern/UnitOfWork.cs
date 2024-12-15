
using Security.Infrastructure.Database;

namespace Security.Infrastructure.Pattern;

public class UnitOfWork : IUnitOfWork
{
    private readonly SecurityContext _context;

    public UnitOfWork(SecurityContext context)
    {
        _context = context;
    }
    public void Dispose()
    {
        _context.Dispose();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
