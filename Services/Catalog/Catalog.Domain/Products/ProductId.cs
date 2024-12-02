using Catalog.Domain.Base;

namespace Catalog.Domain.Products;

public sealed class ProductId : StronglyTypeId
{
    public ProductId(Guid value) : base(value)
    {
    }
}
