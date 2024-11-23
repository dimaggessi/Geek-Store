using FluentValidation;
using GeekStore.Domain.Shared;

namespace GeekStore.Application.Products.Queries.GetProducts;
public class GetProductQueryValidator : AbstractValidator<GetProductsQuery>
{
    public GetProductQueryValidator()
    {
    }
}
