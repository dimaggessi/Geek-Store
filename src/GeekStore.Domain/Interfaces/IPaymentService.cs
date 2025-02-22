using GeekStore.Domain.Entities;

namespace GeekStore.Domain.Interfaces;
public interface IPaymentService
{
    Task<ShoppingCart?> CreateOrUpdatePaymentIntent(ShoppingCart cart, decimal shippingPrice);
    Task<string> RefundPayment(string paymentIntentId);
}
