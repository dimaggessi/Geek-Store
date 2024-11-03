using GeekStore.Domain.Entities;
using GeekStore.Domain.Interfaces;
using GeekStore.Domain.Shared;
using MediatR;

namespace GeekStore.Application.Payments.Commands;

public sealed record CreateOrUpdatePaymentIntentCommand : IRequest<Result<ShoppingCart>>
{
    public string? CartId { get; set; }
}

public class CreateOrUpdatePaymentIntentCommandHandler : IRequestHandler<CreateOrUpdatePaymentIntentCommand, Result<ShoppingCart>>
{
    private readonly IPaymentService _paymentService;

    public CreateOrUpdatePaymentIntentCommandHandler(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }
    public async Task<Result<ShoppingCart>> Handle(CreateOrUpdatePaymentIntentCommand request, CancellationToken cancellationToken)
    {
        var cart = await _paymentService.CreateOrUpdatePaymentIntent(request.CartId!);

        if (cart is null)
            return Result.Failure<ShoppingCart>(new Error(
                ResourceErrorMessages.DEFAULT_NOT_FOUND,
                ResourceErrorMessages.SHOPPING_CART_NULL));

        return Result.Success(cart);
    }
}
