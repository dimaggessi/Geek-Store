using FluentValidation;
using GeekStore.Application.Brands.Queries.GetBrands;

namespace GeekStore.Application.Products.Queries.GetTypes;
public class GetTypesQueryValidator : AbstractValidator<GetTypesQuery>
{
    public GetTypesQueryValidator()
    {
    }
}
