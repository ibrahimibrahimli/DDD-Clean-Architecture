using Application.Common.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace Infrustructure.Services
{
    public sealed class InMemoryCashService : ICacheService
    {
        readonly IMemoryCache _cache;
        readonly HashSet<string> _keys = new();
        readonly SemaphoreSlim _semaphore = new(1,1);
        public InMemoryCashService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default) where T : class
        {
            var result = _cache.TryGetValue(key, out T? value)? value : null;
            return Task.FromResult(result);
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan? expiration = null, CancellationToken cancellationToken = default) where T : class
        {
            var options = new MemoryCacheEntryOptions();
            if (expiration.HasValue)
            {
                options.AbsoluteExpirationRelativeToNow = expiration.Value;
            }
            else
            {
                options.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(50);
            }

            options.RegisterPostEvictionCallback((k, v, reason, state) =>
            {
                _semaphore.Wait();
                try
                {
                    _keys.Remove(k.ToString()!);
                }
                finally
                {
                    _semaphore.Release();
                }
            });

            await _semaphore.WaitAsync(cancellationToken);
            try
            {
                _cache.Set(key, value, options);
                _keys.Add(key);
            }
            finally
            {
                _semaphore?.Release();
            }
        }

        public Task RemoveAsync(string key, CancellationToken cancellationToken = default)
        {
            _cache?.Remove(key);
            _semaphore.Wait(cancellationToken);

            try
            {
                _keys?.Remove(key);
            }
            finally
            {
                _semaphore?.Release();
            }
            return Task.CompletedTask;
        }

        public async Task RemoveByPrefixAsync(string prefix, CancellationToken cancellationToken = default)
        {
            await _semaphore.WaitAsync(cancellationToken);

            try
            {
                var keysToRemove = _keys.Where(k => k.StartsWith(prefix)).ToList();
                foreach (var key in keysToRemove)
                {
                    _cache.Remove(key); 
                    _keys.Remove(key);
                }
            }
            finally
            {
                _semaphore.Release();
            }
        }
    }
}
