namespace Catalog.Domain.Base;

public abstract class StronglyTypeId
{
    private Guid _id;
    public Guid Value { 
        get { return _id; }
        private set {
            if (value == Guid.Empty)
                throw new BusinessRuleException("A Valid id must be provided");
            _id = value; }
    }

    protected StronglyTypeId(Guid value)
    {
        Value = value;
    }
}
