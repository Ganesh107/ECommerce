using StackExchange.Redis;

namespace ECommerce.Product.Service.ProductContext
{
    public class RedisCacheService(IConnectionMultiplexer connection) : ICacheService
    {
        private readonly IDatabase database = connection.GetDatabase();

        public async Task SetAsync(string key, string value, TimeSpan? expiry = null)
        {
            await database.StringSetAsync(key, value, expiry);
        }

        public async Task<string?> GetAsync(string key)
        {
            return await database.StringGetAsync(key);
        }

        public async Task<bool> KeyExistsAsync(string key)
        {
            return await database.KeyExistsAsync(key);
        }

        public async Task RemoveAsync(string key)
        {
            await database.KeyDeleteAsync(key);
        }
    }
}
