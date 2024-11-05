using GeekStore.API.DTOs;
using GeekStore.Domain.Entities.OrderAggregate;

namespace GeekStore.API.Extensions;

public static class OrderMappingExtensions
{
    public static OrderDto ToDto(this Order order)
    {
        return new OrderDto
        {
            Id = order.Id,
            CustomerEmail = order.CustomerEmail,
            OrderDate = order.OrderDate,
            ShippingAddress = order.ShippingAddress,
            DeliveryMethodName = order.DeliveryMethod.Name,
            DeliveryTimeInDays = order.DeliveryMethod.DeliveryTimeInDays,
            ShippingPrice = order.DeliveryMethod.Price,
            PaymentSummary = order.PaymentSummary,
            OrderItems = order.OrderItems.Select(i => i.ToDto()).ToList(),
            SubTotal = order.SubTotal,
            Total = order.GetTotal(),
            Status = order.OrderStatus.ToString(),
            PaymentIntentId = order.PaymentIntentId,
        };
    }

    public static OrderItemDto ToDto(this OrderItem orderItem)
    {
        return new OrderItemDto
        {
            ProductId = orderItem.ItemOrdered.ProductId,
            ProductName = orderItem.ItemOrdered.ProductName,
            Picture = orderItem.ItemOrdered.Picture,
            Price = orderItem.Price,
            Quantity = orderItem.Quantity
        };
    }
}
