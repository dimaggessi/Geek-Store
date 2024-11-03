using GeekStore.Domain.Entities;

namespace GeekStore.Domain.Interfaces;
public interface IPaymentService
{
    Task<ShoppingCart?> CreateOrUpdatePaymentIntent(string cartId);
}
