using GeekStore.Domain.Entities;

namespace GeekStore.Domain.Specifications;
public class BrandSpecification : Specification<Product, string>
{
    public BrandSpecification()
    {
        AddSelect(p => p.Brand);
        ApplyDistinct();
    }
}
