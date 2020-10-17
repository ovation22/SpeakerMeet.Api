using System;
using Ardalis.Specification;
using SpeakerMeet.Core.Constants;
using SpeakerMeet.Core.Entities;

namespace SpeakerMeet.Core.Specifications
{
    public sealed class CommunitySpecification : Specification<Community>
    {
        public CommunitySpecification(Guid id)
        {
            Query.Where(x => x.Id == id);

            WithIncludes();
        }

        public CommunitySpecification(string slug)
        {
            Query.Where(x => x.Slug == slug);

            WithIncludes();
        }

        public CommunitySpecification(int skip, int take, string? direction)
        {
            if (string.Equals(direction, nameof(Direction.Asc), StringComparison.OrdinalIgnoreCase))
            {
                Query.OrderBy(x => x.Name);
            }
            if (string.Equals(direction, nameof(Direction.Desc), StringComparison.OrdinalIgnoreCase))
            {
                Query.OrderByDescending(x => x.Name);
            }

            Query
                .Skip(skip)
                .Take(take);
        }

        private void WithIncludes()
        {
            Query.Include(x => x.CommunityTags).ThenInclude(x => x.Tag);

            Query.Include(x => x.CommunitySocialPlatforms).ThenInclude(x => x.SocialPlatform);
        }
    }
}
