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
using SpeakerMeet.Core.Specifications;

namespace SpeakerMeet.Core.Services
{
    public class CommunityService : ICommunityService
    {
        private readonly int _cacheExpirationMinutes;
        private readonly IDistributedCacheAdapter _cache;
        private readonly ISpeakerMeetRepository _repository;

        public CommunityService(
            IDistributedCacheAdapter cache,
            ISpeakerMeetRepository repository,
            IOptions<CacheConfig> cacheOptions
        )
        {
            _cache = cache;
            _repository = repository;
            _cacheExpirationMinutes = cacheOptions.Value.DefaultExpirationMinutes;
        }

        public async Task<CommunityResult> Get(Guid id)
        {
            var community =  await _repository.Get(new CommunitySpecification(id));

            return new CommunityResult
            {
                Id = community.Id,
                Location = community.Location,
                Name = community.Name,
                Slug = community.Slug,
                Description = community.Description
            };
        }

        public async Task<CommunityResult> Get(string slug)
        {
            var community =  await _repository.Get(new CommunitySpecification(slug));

            return new CommunityResult
            {
                Id = community.Id,
                Location = community.Location,
                Name = community.Name,
                Slug = community.Slug,
                Description = community.Description
            };
        }

        public async Task<IEnumerable<CommunitiesResult>> GetAll()
        {
            var communities = await _repository.GetAll<Community>();

            return communities.Select(x => new CommunitiesResult
            {
                Id = x.Id,
                Location = x.Location,
                Name = x.Name,
                Slug = x.Slug,
                Description = x.Description
            });
        }

        public async Task<IEnumerable<CommunitiesResult>> GetFeatured()
        {
            IEnumerable<CommunitiesResult> results;
            const string cacheKey = CacheKeys.FeaturedCommunities;

            var cacheValue = await _cache.GetStringAsync(cacheKey);

            if (string.IsNullOrEmpty(cacheValue))
            {
                results = (await GetRandomCommunities()).ToList();
                await SetCache(cacheKey, results);
            }
            else
            {
                results = JsonSerializer.Deserialize<List<CommunitiesResult>>(cacheValue);
            }

            return results;
        }

        private async Task SetCache(string cacheKey, IEnumerable<CommunitiesResult> results)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_cacheExpirationMinutes)
            };

            await _cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(results), options);
        }

        private async Task<IEnumerable<CommunitiesResult>> GetRandomCommunities()
        {
            var communities = await _repository.List(new CommunityRandomSpecification());

            var results = communities.Select(x => new CommunitiesResult
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