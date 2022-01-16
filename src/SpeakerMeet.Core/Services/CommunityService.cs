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
    public class CommunityService : ICommunityService
    {
        private readonly ICacheManager _cache;
        private readonly ISpeakerMeetRepository _repository;

        public CommunityService(
            ICacheManager cache,
            ISpeakerMeetRepository repository
        )
        {
            _cache = cache;
            _repository = repository;
        }

        public async Task<CommunityResult> Get(Guid id)
        {
            var community =  await _repository.Get(new CommunitySpecification(id));

            if (community == null)
            {
                throw new EntityNotFoundException();
            }

            return Map(community);
        }

        public async Task<CommunityResult> Get(string slug)
        {
            var community =  await _repository.Get(new CommunitySpecification(slug));

            if (community == null)
            {
                throw new EntityNotFoundException();
            }

            return Map(community);
        }

        public async Task<CommunitiesResult> GetAll(int pageIndex, int itemsPage, string? direction)
        {
            var spec = new CommunitySpecification(itemsPage * pageIndex, itemsPage, direction);

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
                            .ToString(CultureInfo.InvariantCulture))
                }
            };
        }

        public async Task<IEnumerable<CommunityFeatured>> GetFeatured()
        {
            return await _cache.GetOrCreate(CacheKeys.FeaturedCommunities, async () => await GetRandomCommunities());
        }

        private static CommunityResult Map(Community community)
        {
            return new()
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