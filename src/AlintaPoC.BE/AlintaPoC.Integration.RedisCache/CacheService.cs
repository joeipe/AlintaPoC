using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AlintaPoC.Integration.RedisCache
{
    public class CacheService
    {
        private readonly IDistributedCache _cache;

        public CacheService(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task<T> GetFromCacheAsync<T>(string key) where T : class
        {
            var cachedResponse = await _cache.GetStringAsync(key);
            return cachedResponse == null ? null : JsonSerializer.Deserialize<T>(cachedResponse);
        }

        public async Task SetCacheAsync<T>(string key, T value, DistributedCacheEntryOptions distributedCacheOption = null) where T : class
        {
            var options = distributedCacheOption ?? new DistributedCacheEntryOptions()
                                                    {
                                                        SlidingExpiration = TimeSpan.FromSeconds(30),
                                                        AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(100),
                                                    };
            var response = JsonSerializer.Serialize(value);
            await _cache.SetStringAsync(key, response, options);
        }

        public async Task ClearCacheAsync(string key)
        {
            await _cache.RemoveAsync(key);
        }
    }
}
