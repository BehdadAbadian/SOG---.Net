namespace Notification.Infrastructure.Pattern
{
    public interface IUnitOfWork : IDisposable
    {
        public Task SaveChangesAsync();
        public void Dispose();
    }
}
