using GeekStore.Domain.Entities;

namespace GeekStore.Domain.Specifications;
public class TypeSpecification : Specification<Product, string>
{
    public TypeSpecification()
    {
        AddSelect(p => p.Type);
        ApplyDistinct();
    }
}
