using FluentValidation;
using GeekStore.Domain.Shared;

namespace GeekStore.Application.Delivery;
public class GetDeliveryMethodQuerieValidator : AbstractValidator<GetDeliveryMethodQuerie>
{
    public GetDeliveryMethodQuerieValidator()
    {
        RuleFor(d => d.PostalCode)
            .NotEmpty()
            .WithMessage(ResourceErrorMessages.EMPTY_POSTAL_CODE)
            .Matches(@"^\d{8}$")
            .WithMessage(ResourceErrorMessages.VALID_POSTAL_CODE);

        RuleFor(d => d.Products)
            .NotEmpty()
            .NotNull()
            .WithMessage(ResourceErrorMessages.EMPTY_PRODUCT_LIST);

        RuleForEach(d => d.Products).ChildRules(product =>
        {
            product.RuleFor(p => p.Id).NotEmpty().WithMessage(ResourceErrorMessages.EMPTY_PRODUCT_ID);

            product.RuleFor(p => p.Quantity)
            .NotNull().WithMessage(ResourceErrorMessages.EMPTY_PRODUCT_QUANTITY)
            .GreaterThanOrEqualTo(0).WithMessage(ResourceErrorMessages.MIN_PRODUCT_QUANTITY);

            product.RuleFor(p => p.Price)
                .NotNull().WithMessage(ResourceErrorMessages.EMPTY_PRODUCT_PRICE)
                .GreaterThan(0).WithMessage(ResourceErrorMessages.MIN_PRODUCT_PRICE);

            product.RuleFor(p => p.Width)
                .NotNull().WithMessage(ResourceErrorMessages.EMPTY_PRODUCT_WIDTH)
                .GreaterThan(0).WithMessage(ResourceErrorMessages.MIN_PRODUCT_WIDTH);

            product.RuleFor(p => p.Height)
                .NotNull().WithMessage(ResourceErrorMessages.EMPTY_PRODUCT_HEIGHT)
                .GreaterThan(0).WithMessage(ResourceErrorMessages.MIN_PRODUCT_HEIGHT);

            product.RuleFor(p => p.Length)
                .NotNull().WithMessage(ResourceErrorMessages.EMPTY_PRODUCT_LENGTH)
                .GreaterThan(0).WithMessage(ResourceErrorMessages.MIN_PRODUCT_LENGTH);

            product.RuleFor(p => p.Weight)
                .NotNull().WithMessage(ResourceErrorMessages.EMPTY_PRODUCT_WEIGHT)
                .GreaterThan(0).WithMessage(ResourceErrorMessages.MIN_PRODUCT_WEIGHT);
        });
    }
}