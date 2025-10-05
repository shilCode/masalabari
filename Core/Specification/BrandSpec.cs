using System;
using Core.Entities;

namespace Core.Specification;

public class BrandSpec : BaseSpecification<Product, string>
{
public BrandSpec()
{
        AddSelect(x => x.Brand);
}
}
