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

        public async Task<IEnumerable<T>> GetOrCreate<T>(string key, Func<Task<IEnumerable<T>>> createItem) where T : class
        {
            IEnumerable<T> results;
            var cacheEntry = await _cache.GetStringAsync(key);

            if (string.IsNullOrEmpty(cacheEntry))
            {
                results = (await createItem()).ToList();

                await SetCache(key, results);
            }
            else
            {
                results = JsonSerializer.Deserialize<List<T>>(cacheEntry);
            }

            return results;
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
