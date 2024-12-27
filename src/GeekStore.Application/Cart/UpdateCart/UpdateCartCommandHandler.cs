using GeekStore.Domain.Entities;
using GeekStore.Domain.Interfaces;
using GeekStore.Domain.Shared;
using MediatR;

namespace GeekStore.Application.Cart.UpdateCart;

public class UpdateCartCommandHandler(IShoppingCartRepository cartRepository)
        : IRequestHandler<UpdateCartCommand, Result<ShoppingCart>>
    {
        public async Task<Result<ShoppingCart>> Handle(UpdateCartCommand request,
            CancellationToken cancellationToken)
        {
            var cart = new ShoppingCart
            {
                Id = request.Id!,
                Items = request.Items!,
                PostalCode = request.PostalCode,
                DeliveryMethodId = request.DeliveryMethodId,
                ClientSecret = request.ClientSecret,
                PaymentIntentId = request.PaymentIntentId
            };

            var updated = await cartRepository.SetCartAsync(cart);

            if (updated is null)
            {
                return Result.Failure<ShoppingCart>(new Error(
                    ResourceErrorMessages.DEFAULT_ERROR,
                    ResourceErrorMessages.SHOPPING_CART_ERROR));
            }

            return Result.Success(updated);
        }
    }
