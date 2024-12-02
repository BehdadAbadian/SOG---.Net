namespace Catalog.Domain.Base;

public class AggregateRoot<TKey> : Entity<TKey>, IAggregateRoot
{
    public DateTime CreationDate { get; private set; }

    public AggregateRoot()
    {
        CreationDate = DateTime.Now;
    }

}
