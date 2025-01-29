using GeekStore.Domain.Entities.OrderAggregate;

namespace GeekStore.API.DTOs;

public class CreateOrderDto
{
    public string? CustomerEmail { get; set; }
    public string? CartId { get; set; }
    public int? DeliveryMethodId { get; set; }
    public ShippingAddress? ShippingAddress { get; set; }
    public PaymentSummary? PaymentSummary { get; set; }
}
