using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
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
        private readonly ICacheManager _cache;
        private readonly ISpeakerMeetRepository _repository;

        public SpeakerService(
            ICacheManager cache,
            ISpeakerMeetRepository repository
        )
        {
            _cache = cache;
            _repository = repository;
        }

        public async Task<SpeakerResult> Get(Guid id)
        {
            var speaker =  await _repository.Get(new SpeakerSpecification(id));

            return Map(speaker);
        }

        public async Task<SpeakerResult> Get(string slug)
        {
            var speaker =  await _repository.Get(new SpeakerSpecification(slug));

            return Map(speaker);
        }

        public async Task<SpeakersResult> GetAll(int pageIndex, int itemsPage, string? direction)
        {
            var spec = new SpeakerSpecification(itemsPage * pageIndex, itemsPage, direction);

            var speakers = await _repository.List(spec);
            var total = await _repository.Count<Speaker>();

            return new SpeakersResult
            {
                Speakers = speakers.Select(x => new SpeakersResult.Speaker
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
                    ItemsPerPage = speakers.Count,
                    TotalItems = total,
                    TotalPages =
                        int.Parse(Math.Ceiling((decimal)total / itemsPage)
                            .ToString(CultureInfo.InvariantCulture))
                }
            };
        }

        public async Task<IEnumerable<SpeakerFeatured>> GetFeatured()
        {
            return await _cache.GetOrCreate(CacheKeys.FeaturedSpeakers, async () => await GetRandomSpeakers());
        }

        private static SpeakerResult Map(Speaker speaker)
        {
            return new()
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

        private async Task<IEnumerable<SpeakerFeatured>> GetRandomSpeakers()
        {
            var communities = await _repository.List(new SpeakerRandomSpecification());

            var results = communities.Select(x => new SpeakerFeatured
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