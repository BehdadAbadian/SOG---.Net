namespace Catalog.Domain.Base;

public abstract class Entity<TKey>
{
    public TKey Id { get; protected set; }
}
