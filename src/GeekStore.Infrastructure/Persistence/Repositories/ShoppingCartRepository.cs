using GeekStore.Domain.Entities;
using GeekStore.Domain.Interfaces;
using StackExchange.Redis;
using System.Text.Json;

namespace GeekStore.Infrastructure.Persistence.Repositories;
public class ShoppingCartRepository : IShoppingCartRepository
{
    private readonly IDatabase _database;

    public ShoppingCartRepository(IConnectionMultiplexer redis)
    {
        _database = redis.GetDatabase();
    }

    public async Task<ShoppingCart?> GetCartAsync(string cartId)
    {
        var data = await _database.StringGetAsync(cartId);

        return data.IsNullOrEmpty ? 
            null : JsonSerializer.Deserialize<ShoppingCart>(data!);
    }

    public async Task<ShoppingCart?> SetCartAsync(ShoppingCart cart)
    {
        var createdCart = await _database.StringSetAsync(cart.Id, 
            JsonSerializer.Serialize(cart), TimeSpan.FromDays(7));

        return createdCart ? await GetCartAsync(cart.Id) : null;


    }
    public async Task<bool> DeleteCartAsync(string cartId)
    {
        return await _database.KeyDeleteAsync(cartId);
    }
}
