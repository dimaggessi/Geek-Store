using GeekStore.Domain.Entities.OrderAggregate;

namespace GeekStore.API.DTOs;

public class OrderDto
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; }
    public string? CustomerEmail { get; set; }
    public ShippingAddress? ShippingAddress { get; set; }
    public string? DeliveryMethodName { get; set; }
    public int DeliveryTimeInDays { get; set; }
    public decimal ShippingPrice { get; set; }
    public PaymentSummary? PaymentSummary { get; set; }
    public List<OrderItemDto> OrderItems { get; set; } = [];
    public decimal SubTotal { get; set; }
    public decimal Total { get; set; }
    public string? Status { get; set; }
    public string? PaymentIntentId { get; set; }
}
