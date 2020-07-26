using System;
using System.Collections.Generic;
using System.Linq;
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
    public class ConferenceService : IConferenceService
    {
        private readonly int _cacheExpirationMinutes;
        private readonly IDistributedCacheAdapter _cache;
        private readonly ISpeakerMeetRepository _repository;

        public ConferenceService(
            IDistributedCacheAdapter cache,
            ISpeakerMeetRepository repository,
            IOptions<CacheConfig> cacheOptions
        )
        {
            _cache = cache;
            _repository = repository;
            _cacheExpirationMinutes = cacheOptions.Value.DefaultExpirationMinutes;
        }

        public async Task<ConferenceResult> Get(Guid id)
        {
            var conference =  await _repository.Get<Conference>(x => x.Id == id);

            return new ConferenceResult
            {
                Id = conference.Id,
                Location = conference.Location,
                Name = conference.Name,
                Slug = conference.Slug,
                Description = conference.Description
            };
        }

        public async Task<ConferenceResult> Get(string slug)
        {
            var conference =  await _repository.Get<Conference>(x => x.Slug == slug);

            return new ConferenceResult
            {
                Id = conference.Id,
                Location = conference.Location,
                Name = conference.Name,
                Slug = conference.Slug,
                Description = conference.Description
            };
        }

        public async Task<IEnumerable<ConferencesResult>> GetAll()
        {
            var conferences = await _repository.GetAll<Conference>();

            return conferences.Select(x => new ConferencesResult
            {
                Id = x.Id,
                Location = x.Location,
                Name = x.Name,
                Slug = x.Slug,
                Description = x.Description
            });
        }

        public async Task<IEnumerable<ConferencesResult>> GetFeatured()
        {
            IEnumerable<ConferencesResult> results;
            const string cacheKey = CacheKeys.FeaturedConferences;

            var cacheValue = await _cache.GetStringAsync(cacheKey);

            if (string.IsNullOrEmpty(cacheValue))
            {
                results = (await GetRandomConferences()).ToList();
                await SetCache(cacheKey, results);
            }
            else
            {
                results = JsonSerializer.Deserialize<List<ConferencesResult>>(cacheValue);
            }

            return results;
        }

        private async Task SetCache(string cacheKey, IEnumerable<ConferencesResult> results)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_cacheExpirationMinutes)
            };

            await _cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(results), options);
        }

        private async Task<IEnumerable<ConferencesResult>> GetRandomConferences()
        {
            var conferences = await _repository.GetRandom<Conference>(4);

            var results = conferences.Select(x => new ConferencesResult
            {
                Id = x.Id,
                Location = x.Location,
                Name = x.Name,
                Slug = x.Slug,
                Description = x.Description
            });

            return results;
        }
    }
}