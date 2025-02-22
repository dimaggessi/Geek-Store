using FluentValidation;

namespace GeekStore.Application.Products.Queries.GetProducts;
public class GetProductsQueryValidator : AbstractValidator<GetProductsQuery>
{
    public GetProductsQueryValidator()
    {
    }
}
