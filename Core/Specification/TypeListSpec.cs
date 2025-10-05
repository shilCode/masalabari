using System;
using Core.Entities;

namespace Core.Specification;

public class TypeListSpec : BaseSpecification<Product,string>
{
    public TypeListSpec()
    {
        AddSelect(x => x.Type);
        ApplyDistinct();
    }
}
