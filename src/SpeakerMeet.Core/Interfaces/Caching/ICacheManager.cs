using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpeakerMeet.Core.Interfaces.Caching
{
    public interface ICacheManager
    {
        Task<T> GetOrCreate<T>(string key, Func<Task<T>> createItem) where T : class;
        Task<IEnumerable<T>> GetOrCreate<T>(string key, Func<Task<IEnumerable<T>>> createItem) where T : class;
        Task Remove(string key);
    }
}
