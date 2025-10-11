using System;
using Core.Entities;

namespace Core.Specification;

public class ProductFilterSpec : BaseSpecification<Product>
{
    public ProductFilterSpec(ProductSpecParam specParam) : base(
        x => (string.IsNullOrEmpty(specParam.Search) || x.Name.ToLower().Contains(specParam.Search)) && (specParam.Brands.Count == 0 || specParam.Brands.Contains(x.Brand)) &&
        (specParam.Types.Count == 0 || specParam.Types.Contains(x.Type))
    )
    {
        ApplyPaging(specParam.PageSize * (specParam.PageIndex - 1), specParam.PageSize);
        switch (specParam.Sort)
        {
            case "priceAsc":
                AddOrderBy(x => x.Price);
                break;
            case "priceDesc":
                AddOrderByDesc(x => x.Price);
                break;
            default:
                AddOrderBy(x => x.Name);
                break;
        }
    }
}
