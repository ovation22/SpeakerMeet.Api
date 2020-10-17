using System;
using Ardalis.Specification;
using SpeakerMeet.Core.Constants;
using SpeakerMeet.Core.Entities;

namespace SpeakerMeet.Core.Specifications
{
    public sealed class SpeakerSpecification : Specification<Speaker>
    {
        public SpeakerSpecification(Guid id)
        {
            Query.Where(x => x.Id == id);

            WithIncludes();
        }

        public SpeakerSpecification(string slug)
        {
            Query.Where(x => x.Slug == slug);

            WithIncludes();
        }

        public SpeakerSpecification(int skip, int take, string? direction)
        {
            if (string.Equals(direction, nameof(Direction.Asc), StringComparison.InvariantCultureIgnoreCase))
            {
                Query.OrderBy(x => x.Name);
            }
            if (string.Equals(direction, nameof(Direction.Desc), StringComparison.InvariantCultureIgnoreCase))
            {
                Query.OrderByDescending(x => x.Name);
            }

            Query
                .Skip(skip)
                .Take(take);
        }

        private void WithIncludes()
        {
            Query.Include(x => x.SpeakerTags).ThenInclude(x => x.Tag);

            Query.Include(x => x.SpeakerSocialPlatforms).ThenInclude(x => x.SocialPlatform);
        }
    }
}