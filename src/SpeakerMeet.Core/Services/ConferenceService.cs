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
            var conference =  await _repository.Get(new ConferenceSpecification(id));

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
            var conference =  await _repository.Get(new ConferenceSpecification(slug));

            return new ConferenceResult
            {
                Id = conference.Id,
                Location = conference.Location,
                Name = conference.Name,
                Slug = conference.Slug,
                Description = conference.Description,
                Tags = conference.ConferenceTags.Select(x => x.Tag.Name),
                SocialPlatforms = conference.ConferenceSocialPlatforms.Select(x => new SocialMedia
                {
                    Name = x.SocialPlatform.Name,
                    Url = x.Url
                })
            };
        }

        public async Task<ConferencesResult> GetAll(int pageIndex, int itemsPage)
        {
            var spec = new ConferenceSpecification(itemsPage * pageIndex, itemsPage);

            var conferences = await _repository.List(spec);
            var total = await _repository.Count<Conference>();

            return new ConferencesResult
            {
                Conferences = conferences.Select(x => new ConferencesResult.Conference
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
                    ItemsPerPage = conferences.Count,
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

        public async Task<IEnumerable<ConferenceFeatured>> GetFeatured()
        {
            IEnumerable<ConferenceFeatured> results;
            const string cacheKey = CacheKeys.FeaturedConferences;

            var cacheValue = await _cache.GetStringAsync(cacheKey);

            if (string.IsNullOrEmpty(cacheValue))
            {
                results = (await GetRandomConferences()).ToList();
                await SetCache(cacheKey, results);
            }
            else
            {
                results = JsonSerializer.Deserialize<List<ConferenceFeatured>>(cacheValue);
            }

            return results;
        }

        private async Task SetCache(string cacheKey, IEnumerable<ConferenceFeatured> results)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_cacheExpirationMinutes)
            };

            await _cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(results), options);
        }

        private async Task<IEnumerable<ConferenceFeatured>> GetRandomConferences()
        {
            var conferences = await _repository.List(new ConferenceRandomSpecification());

            var results = conferences.Select(x => new ConferenceFeatured
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