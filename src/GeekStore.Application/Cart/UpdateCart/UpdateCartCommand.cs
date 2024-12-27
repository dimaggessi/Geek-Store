using GeekStore.Domain.Entities;
using GeekStore.Domain.Shared;
using MediatR;
using System.Text.Json.Serialization;

namespace GeekStore.Application.Cart.UpdateCart
{
    public sealed record UpdateCartCommand : IRequest<Result<ShoppingCart>>
    {
        public string? Id { get; init; }
        public int? DeliveryMethodId { get; set; }
        public string? PostalCode { get; set; }
        public string? ClientSecret { get; set; }
        public string? PaymentIntentId { get; set; }

        [JsonPropertyName("Items")]
        public List<CartItem>? Items { get; init; }
    }
}
