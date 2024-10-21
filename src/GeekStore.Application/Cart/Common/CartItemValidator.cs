using FluentValidation;
using GeekStore.Domain.Entities;
using GeekStore.Domain.Shared;

namespace GeekStore.Application.Cart.Common;
public class CartItemValidator : AbstractValidator<CartItem>
{
    public CartItemValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty().WithMessage(ResourceErrorMessages.EMPTY_PRODUCT_ID)
            .GreaterThan(0).WithMessage(ResourceErrorMessages.EMPTY_PRODUCT_ID);

        RuleFor(x => x.ProductName)
            .NotEmpty().WithMessage(ResourceErrorMessages.EMPTY_PRODUCT_NAME)
            .MaximumLength(100).WithMessage(ResourceErrorMessages.MAX_PRODUCT_NAME);

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage(ResourceErrorMessages.MIN_PRODUCT_PRICE);

        RuleFor(x => x.Quantity)
            .NotEmpty().WithMessage(ResourceErrorMessages.EMPTY_PRODUCT_QUANTITY)
            .GreaterThan(0).WithMessage(ResourceErrorMessages.MIN_PRODUCT_QUANTITY);

        RuleFor(x => x.Picture)
            .NotEmpty().WithMessage(ResourceErrorMessages.EMPTY_PRODUCT_PICTURE)
            .Must(p => Uri.IsWellFormedUriString(p, UriKind.RelativeOrAbsolute))
            .WithMessage("Picture must be a valid URI.");

        RuleFor(x => x.Brand)
            .NotEmpty().WithMessage(ResourceErrorMessages.EMPTY_PRODUCT_BRAND)
            .MaximumLength(50).WithMessage(ResourceErrorMessages.MAX_PRODUCT_BRAND);

        RuleFor(x => x.Type)
            .NotEmpty().WithMessage(ResourceErrorMessages.EMPTY_PRODUCT_TYPE)
            .MaximumLength(50).WithMessage(ResourceErrorMessages.MAX_PRODUCT_TYPE);
    }
}
