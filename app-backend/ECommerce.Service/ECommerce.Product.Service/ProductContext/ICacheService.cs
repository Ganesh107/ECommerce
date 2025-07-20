namespace ECommerce.Product.Service.ProductContext
{
    public interface ICacheService
    {
        Task SetAsync(string key, string value, TimeSpan? expiry = null);
        Task<string?> GetAsync(string key);
        Task<bool> KeyExistsAsync(string key);
        Task RemoveAsync(string key);
    }
}
