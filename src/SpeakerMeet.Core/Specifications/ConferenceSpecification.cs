using System;
using Ardalis.Specification;
using SpeakerMeet.Core.Constants;
using SpeakerMeet.Core.Entities;

namespace SpeakerMeet.Core.Specifications
{
    public sealed class ConferenceSpecification : Specification<Conference>
    {
        public ConferenceSpecification(Guid id)
        {
            Query.Where(x => x.Id == id);

            WithIncludes();
        }

        public ConferenceSpecification(string slug)
        {
            Query
                .AsNoTracking()
                .Where(x => x.Slug == slug);

            WithIncludes();
        }

        public ConferenceSpecification(int skip, int take, string? direction)
        {
            if (string.Equals(direction, nameof(Direction.Desc), StringComparison.OrdinalIgnoreCase))
            {
                Query
                    .AsNoTracking()
                    .OrderByDescending(x => x.Name)
                    .Skip(skip)
                    .Take(take);
            }
            else
            {
                Query
                    .AsNoTracking()
                    .OrderBy(x => x.Name)
                    .Skip(skip)
                    .Take(take);
            }
        }

        private void WithIncludes()
        {
            Query.Include(x => x.ConferenceTags).ThenInclude(x => x.Tag);

            Query.Include(x => x.ConferenceSocialPlatforms).ThenInclude(x => x.SocialPlatform);
        }
    }
}