using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpeakerMeet.Core.Interfaces.Caching
{
    public interface ICacheManager
    {
        Task<T> GetOrCreate<T>(string key, Func<Task<T>> createItem) where T : class;
        Task<IReadOnlyCollection<T>> GetOrCreate<T>(string key, Func<Task<IReadOnlyCollection<T>>> createItem) where T : class;
        Task Remove(string key);
    }
}
