namespace Security.Infrastructure.Pattern
{
    public interface IUnitOfWork : IDisposable
    {
        public Task SaveChangesAsync();
        public void Dispose();

    }
}
