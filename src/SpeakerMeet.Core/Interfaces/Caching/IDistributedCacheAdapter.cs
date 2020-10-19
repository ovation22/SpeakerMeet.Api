using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;

namespace SpeakerMeet.Core.Interfaces.Caching
{
    public interface IDistributedCacheAdapter
    {
        Task<string> GetStringAsync(string key);

        Task SetStringAsync(string key, string value, DistributedCacheEntryOptions options);
        Task RemoveAsync(string key);
    }
}
