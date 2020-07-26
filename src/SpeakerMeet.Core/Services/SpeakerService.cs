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
    public class SpeakerService : ISpeakerService
    {
        private readonly int _cacheExpirationMinutes;
        private readonly IDistributedCacheAdapter _cache;
        private readonly ISpeakerMeetRepository _repository;

        public SpeakerService(
            IDistributedCacheAdapter cache,
            ISpeakerMeetRepository repository,
            IOptions<CacheConfig> cacheOptions
        )
        {
            _cache = cache;
            _repository = repository;
            _cacheExpirationMinutes = cacheOptions.Value.DefaultExpirationMinutes;
        }

        public async Task<SpeakerResult> Get(Guid id)
        {
            var speaker =  await _repository.Get<Speaker>(x => x.Id == id);

            return new SpeakerResult
            {
                Id = speaker.Id,
                Location = speaker.Location,
                Name = speaker.Name,
                Slug = speaker.Slug,
                Description = speaker.Description
            };
        }

        public async Task<SpeakerResult> Get(string slug)
        {
            var speaker =  await _repository.Get<Speaker>(x => x.Slug == slug);

            return new SpeakerResult
            {
                Id = speaker.Id,
                Location = speaker.Location,
                Name = speaker.Name,
                Slug = speaker.Slug,
                Description = speaker.Description
            };
        }

        public async Task<IEnumerable<SpeakersResult>> GetAll()
        {
            var speakers = await _repository.GetAll<Speaker>();

            return speakers.Select(x => new SpeakersResult
            {
                Id = x.Id,
                Location = x.Location,
                Name = x.Name,
                Slug = x.Slug,
                Description = x.Description
            });
        }

        public async Task<IEnumerable<SpeakersResult>> GetFeatured()
        {
            IEnumerable<SpeakersResult> results;
            const string cacheKey = CacheKeys.FeaturedSpeakers;

            var cacheValue = await _cache.GetStringAsync(cacheKey);

            if (string.IsNullOrEmpty(cacheValue))
            {
                results = (await GetRandomSpeakers()).ToList();
                await SetCache(cacheKey, results);
            }
            else
            {
                results = JsonSerializer.Deserialize<List<SpeakersResult>>(cacheValue);
            }

            return results;
        }

        private async Task SetCache(string cacheKey, IEnumerable<SpeakersResult> results)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_cacheExpirationMinutes)
            };

            await _cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(results), options);
        }

        private async Task<IEnumerable<SpeakersResult>> GetRandomSpeakers()
        {
            var speakers = await _repository.GetRandom<Speaker>(4);

            var results = speakers.Select(x => new SpeakersResult
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