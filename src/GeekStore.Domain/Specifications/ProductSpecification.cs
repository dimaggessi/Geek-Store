using GeekStore.Domain.Entities;
using GeekStore.Domain.Specifications.SpecParams;

namespace GeekStore.Domain.Specifications
{
    public class ProductSpecification : Specification<Product>
    {
        public ProductSpecification(ProductSpecParams specParams) : base(product =>
            (!specParams.MinPrice.HasValue || product.Price >= specParams.MinPrice.Value) &&
            (!specParams.MaxPrice.HasValue || product.Price <= specParams.MaxPrice.Value) &&
            (string.IsNullOrEmpty(specParams.Search) || product.Name.ToLower().Contains(specParams.Search)) &&
            (specParams.Brands.Count == 0) || specParams.Brands.Contains(product.Brand) &&
            (specParams.Types.Count == 0 || specParams.Types.Contains(product.Type)))
            {
                var skip = specParams.PageSize * (specParams.PageIndex - 1);
                var take = specParams.PageSize;

                AddPagination(skip, take);

                switch (specParams.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(product => product.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(product => product.Price);
                        break;
                    default:
                        AddOrderBy(product => product.Name);
                        break;
                }
            }
    }
}