using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using SpeakerMeet.Core.Interfaces.Caching;

namespace SpeakerMeet.Infrastructure.Caching
{
    public class DistributedCacheAdapter : IDistributedCacheAdapter
    {
        private readonly IDistributedCache _cache;

        public DistributedCacheAdapter(IDistributedCache cache)
        {
            _cache = cache;
        }

        public Task<string> GetStringAsync(string key)
        {
            return _cache.GetStringAsync(key);
        }

        public Task SetStringAsync(string key, string value, DistributedCacheEntryOptions options)
        {
            return _cache.SetStringAsync(key, value, options);
        }
    }
}
