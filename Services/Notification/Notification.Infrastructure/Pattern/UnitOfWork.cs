
using Notification.Infrastructure.Database;

namespace Notification.Infrastructure.Pattern;

public class UnitOfWork : IUnitOfWork
{
    private readonly NotificationContext _context;

    public UnitOfWork(NotificationContext context)
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
