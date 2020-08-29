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
            var speaker =  await _repository.Get(new SpeakerSpecification(id));

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
            var speaker =  await _repository.Get(new SpeakerSpecification(slug));

            return new SpeakerResult
            {
                Id = speaker.Id,
                Location = speaker.Location,
                Name = speaker.Name,
                Slug = speaker.Slug,
                Description = speaker.Description,
                Tags = speaker.SpeakerTags.Select(x => x.Tag.Name),
                SocialPlatforms = speaker.SpeakerSocialPlatforms.Select(x => new SocialMedia
                {
                    Name = x.SocialPlatform.Name,
                    Url = x.Url
                })
            };
        }

        public async Task<IEnumerable<SpeakersResult>> GetAll(int pageIndex, int itemsPage)
        {
            var spec = new SpeakerSpecification(itemsPage * pageIndex, itemsPage);

            var speakers = await _repository.List(spec);
            var total = await _repository.Count<Speaker>();

            return speakers.Select(x => new SpeakersResult
            {
                Id = x.Id,
                Location = x.Location,
                Name = x.Name,
                Slug = x.Slug,
                Description = x.Description,
                PaginationInfo = new PaginationInfo
                {
                    ActualPage = pageIndex,
                    ItemsPerPage = speakers.Count,
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
            var speakers = await _repository.List(new SpeakerRandomSpecification());

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