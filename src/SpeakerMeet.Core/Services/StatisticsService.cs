using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using SpeakerMeet.Core.Cache;
using SpeakerMeet.Core.DTOs;
using SpeakerMeet.Core.Entities;
using SpeakerMeet.Core.Interfaces.Caching;
using SpeakerMeet.Core.Interfaces.Repositories;
using SpeakerMeet.Core.Interfaces.Services;

namespace SpeakerMeet.Core.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly IDistributedCacheAdapter _cache;
        private readonly ISpeakerMeetRepository _repository;
        private readonly int _cacheExpirationMinutes;

        public StatisticsService(
            IDistributedCacheAdapter cache,
            ISpeakerMeetRepository repository,
            IOptions<CacheConfig> cacheOptions
        )
        {
            _cache = cache;
            _repository = repository;
            _cacheExpirationMinutes = cacheOptions.Value.DefaultExpirationMinutes;
        }

        public async Task<CountResult> GetCounts()
        {
            CountResult result;
            const string cacheKey = CacheKeys.Counts;

            var cacheValue = await _cache.GetStringAsync(cacheKey);

            if (string.IsNullOrEmpty(cacheValue))
            {
                result = await GetCountResult();
                await SetCache(cacheKey, result);
            }
            else
            {
                result = JsonSerializer.Deserialize<CountResult>(cacheValue);
            }

            return result;
        }

        private async Task SetCache(string cacheKey, CountResult result)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_cacheExpirationMinutes)
            };

            await _cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(result), options);
        }

        private async Task<CountResult> GetCountResult()
        {
            CountResult result;
            var speakers = await _repository.Count<Speaker>();
            var communities = await _repository.Count<Community>();
            var conferences = await _repository.Count<Conference>();

            result = new CountResult
            {
                SpeakerCount = speakers,
                CommunityCount = communities,
                ConferenceCount = conferences
            };
            return result;
        }
    }
}