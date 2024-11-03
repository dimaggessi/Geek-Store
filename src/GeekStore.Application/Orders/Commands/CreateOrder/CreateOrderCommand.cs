using GeekStore.Domain.Entities.OrderAggregate;
using GeekStore.Domain.Shared;
using MediatR;

namespace GeekStore.Application.Orders.Commands.CreateOrder;
public sealed record CreateOrderCommand : IRequest<Result<Order>>
{
    public string? CustomerEmail { get; set; }
    public string? CartId { get; set; }
    public int? DeliveryMethodId { get; set; }
    public ShippingAddress? ShippingAddress { get; set; }
    public PaymentSummary? PaymentSummary { get; set; }
}
