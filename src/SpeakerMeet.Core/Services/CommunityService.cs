using System;
using System.Collections.Generic;
using System.Globalization;
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
                Description = community.Description,
                Tags = community.CommunityTags.Select(x => x.Tag.Name),
                SocialPlatforms = community.CommunitySocialPlatforms.Select(x => new SocialMedia
                {
                    Name = x.SocialPlatform.Name,
                    Url = x.Url
                })
            };
        }

        public async Task<CommunitiesResult> GetAll(int pageIndex, int itemsPage)
        {
            var spec = new CommunitySpecification(itemsPage * pageIndex, itemsPage);

            var communities = await _repository.List(spec);
            var total = await _repository.Count<Community>();

            return new CommunitiesResult{
                Communities = communities.Select(x => new CommunitiesResult.Community
                {
                    Id = x.Id,
                    Location = x.Location,
                    Name = x.Name,
                    Slug = x.Slug,
                    Description = x.Description,
                }),
                PaginationInfo = new PaginationInfo
                {
                    ActualPage = pageIndex,
                    ItemsPerPage = communities.Count,
                    TotalItems = total,
                    TotalPages =
                        int.Parse(Math.Ceiling((decimal)total / itemsPage)
                            .ToString(CultureInfo.InvariantCulture)),
                    Next = pageIndex == int.Parse(Math.Ceiling((decimal)total / itemsPage)
                        .ToString(CultureInfo.InvariantCulture)) - 1
                        ? "is-disabled"
                        : "",
                    Previous = pageIndex == 0 ? "is-disabled" : ""
                }
            };
        }

        public async Task<IEnumerable<CommunityFeatured>> GetFeatured()
        {
            IEnumerable<CommunityFeatured> results;
            const string cacheKey = CacheKeys.FeaturedCommunities;

            var cacheValue = await _cache.GetStringAsync(cacheKey);

            if (string.IsNullOrEmpty(cacheValue))
            {
                results = (await GetRandomCommunities()).ToList();
                await SetCache(cacheKey, results);
            }
            else
            {
                results = JsonSerializer.Deserialize<List<CommunityFeatured>>(cacheValue);
            }

            return results;
        }

        private async Task SetCache(string cacheKey, IEnumerable<CommunityFeatured> results)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_cacheExpirationMinutes)
            };

            await _cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(results), options);
        }

        private async Task<IEnumerable<CommunityFeatured>> GetRandomCommunities()
        {
            var communities = await _repository.List(new CommunityRandomSpecification());

            var results = communities.Select(x => new CommunityFeatured
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