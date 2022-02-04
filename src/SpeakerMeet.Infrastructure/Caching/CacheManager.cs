using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using SpeakerMeet.Core.Cache;
using SpeakerMeet.Core.Interfaces.Caching;

namespace SpeakerMeet.Infrastructure.Caching
{
    public class CacheManager : ICacheManager
    {
        private readonly int _cacheExpirationMinutes;
        private readonly IDistributedCacheAdapter _cache;

        public CacheManager(
            IDistributedCacheAdapter cache,
            IOptions<CacheConfig> cacheOptions
        )
        {
            _cache = cache;
            _cacheExpirationMinutes = cacheOptions.Value.DefaultExpirationMinutes;
        }

        public async Task<T> GetOrCreate<T>(string key, Func<Task<T>> createItem) where T : class
        {
            T result;
            var cacheEntry = await _cache.GetStringAsync(key);

            if (string.IsNullOrEmpty(cacheEntry))
            {
                result = (await createItem());

                await SetCache(key, result);
            }
            else
            {
                result = JsonSerializer.Deserialize<T>(cacheEntry)!;
            }

            return result!;
        }

        public async Task<IReadOnlyCollection<T>> GetOrCreate<T>(string key, Func<Task<IReadOnlyCollection<T>>> createItem) where T : class
        {
            IReadOnlyCollection<T> results;
            var cacheEntry = await _cache.GetStringAsync(key);

            if (string.IsNullOrEmpty(cacheEntry))
            {
                results = (await createItem()).ToList();

                await SetCache(key, results);
            }
            else
            {
                results = JsonSerializer.Deserialize<List<T>>(cacheEntry)!;
            }

            return results;
        }

        public async Task Remove(string key)
        {
            await _cache.RemoveAsync(key);
        }

        private async Task SetCache<T>(string cacheKey, T results) where T : class
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_cacheExpirationMinutes)
            };

            await _cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(results), options);
        }

        private async Task SetCache<T>(string cacheKey, IEnumerable<T> results) where T : class
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_cacheExpirationMinutes)
            };

            await _cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(results), options);
        }
    }
}
