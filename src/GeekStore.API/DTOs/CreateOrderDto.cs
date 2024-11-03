using GeekStore.Domain.Entities.OrderAggregate;
using System.ComponentModel.DataAnnotations;

namespace GeekStore.API.DTOs;

public class CreateOrderDto
{
    public string? CartId { get; set; }
    public int? DeliveryMethodId { get; set; }
    public ShippingAddress? ShippingAddress { get; set; }
    public PaymentSummary? PaymentSummary { get; set; }
}
