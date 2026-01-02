using Application.Common.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Infrustructure.Services
{
    public sealed class RedisCacheService : ICacheService
    {
        readonly IDistributedCache _cache;

        public RedisCacheService(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default) where T : class
        {
            var cachedValue = await _cache.GetStringAsync(key, cancellationToken);
            if (string.IsNullOrEmpty(cachedValue))
                return null;

            return JsonSerializer.Deserialize<T>(cachedValue);

        }

        public async Task SetAsync<T>(string key, T value, TimeSpan? expiration = null, CancellationToken cancellationToken = default) where T : class
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expiration ?? TimeSpan.FromMinutes(50),
            };

            var serializedValue = JsonSerializer.Serialize(value);
            await _cache.SetStringAsync(key, serializedValue, options, cancellationToken);
        }

        public async Task RemoveAsync(string key, CancellationToken cancellationToken = default)
        {
            await _cache.RemoveAsync(key, cancellationToken);
        }

        public Task RemoveByPrefixAsync(string prefix, CancellationToken cancellationToken = default)
        {
            // Redis-də prefix-lə silmək üçün Redis specific implementation lazımdlr
            // Bu sadə nümumədə implement edilməyib
            throw new NotImplementedException("Redis prefix removal requires Redis-specific implementation");
        }
    }
}
