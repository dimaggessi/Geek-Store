using FluentValidation;
using GeekStore.Application.Brands.Queries.GetBrands;

namespace GeekStore.Application.Products.Queries.GetBrands;
public class GetBrandsQueryValidator : AbstractValidator<GetBrandsQuery>
{
    public GetBrandsQueryValidator()
    {
    }
}
