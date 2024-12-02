using Catalog.Domain.Base;

namespace Catalog.Domain.Categories;

public sealed class CategoryId : StronglyTypeId
{
    public CategoryId(Guid value) : base(value)
    {
    }
}
