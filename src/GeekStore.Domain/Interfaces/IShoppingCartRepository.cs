using GeekStore.Domain.Entities;

namespace GeekStore.Domain.Interfaces
{
    public interface IShoppingCartRepository
    {
        Task<ShoppingCart?> GetCartAsync(string cartId);
        Task<ShoppingCart?> SetCartAsync(ShoppingCart cart);
        Task<bool> DeleteCartAsync(string cartId);
    }
}
