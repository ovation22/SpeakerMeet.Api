using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using SpeakerMeet.Core.Cache;
using SpeakerMeet.Core.DTOs;
using SpeakerMeet.Core.Entities;
using SpeakerMeet.Core.Exceptions;
using SpeakerMeet.Core.Interfaces.Caching;
using SpeakerMeet.Core.Interfaces.Repositories;
using SpeakerMeet.Core.Interfaces.Services;
using SpeakerMeet.Core.Specifications;

namespace SpeakerMeet.Core.Services
{
    public class ConferenceService : IConferenceService
    {
        private readonly ICacheManager _cache;
        private readonly ISpeakerMeetRepository _repository;

        public ConferenceService(
            ICacheManager cache,
            ISpeakerMeetRepository repository
        )
        {
            _cache = cache;
            _repository = repository;
        }

        public async Task<ConferenceResult> Get(Guid id)
        {
            var conference =  await _repository.Get(new ConferenceSpecification(id));

            if (conference == null)
            {
                throw new EntityNotFoundException();
            }

            return Map(conference);
        }

        public async Task<ConferenceResult> Get(string slug)
        {
            var conference =  await _repository.Get(new ConferenceSpecification(slug));

            if (conference == null)
            {
                throw new EntityNotFoundException();
            }

            return Map(conference);
        }

        public async Task<ConferencesResult> GetAll(int pageIndex, int itemsPage, string? direction)
        {
            var spec = new ConferenceSpecification(itemsPage * pageIndex, itemsPage, direction);

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
                            .ToString(CultureInfo.InvariantCulture))
                }
            };
        }

        public async Task<IEnumerable<ConferenceFeatured>> GetFeatured()
        {
            return await _cache.GetOrCreate(CacheKeys.FeaturedConferences, async () => await GetRandomConferences());
        }

        private static ConferenceResult Map(Conference conference)
        {
            return new()
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