using GeekStore.Domain.Entities;
using System.Text.Json.Serialization;

namespace GeekStore.API.DTOs;
public class CartDto
{
    public string? CartId { get; init; }
    public int? DeliveryMethodId { get; set; }
    public string? PostalCode { get; set; }
    public List<CartItem>? Items { get; init; }
}
