using GeekStore.Domain.Entities;
using GeekStore.Domain.Interfaces;
using GeekStore.Domain.Shared;
using MediatR;
using System.Text.Json.Serialization;

namespace GeekStore.Application.Cart.UpdateCart;

public sealed record UpdateCartCommand : IRequest<Result<ShoppingCart>>
{
    public required string Id { get; init; }

    [JsonPropertyName("Items")]
    public List<CartItem> Items { get; init; } = [];
}

public class UpdateCartCommandHandler(IShoppingCartRepository cartRepository)
        : IRequestHandler<UpdateCartCommand, Result<ShoppingCart>>
    {
        public async Task<Result<ShoppingCart>> Handle(UpdateCartCommand request,
            CancellationToken cancellationToken)
        {
            var cart = new ShoppingCart
            {
                Id = request.Id,
                Items = request.Items,
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
