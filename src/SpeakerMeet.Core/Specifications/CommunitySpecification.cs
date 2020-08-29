using System;
using Ardalis.Specification;
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

        public CommunitySpecification(int skip, int take)
        {
            Query
                .Paginate(skip, take);
        }

        private void WithIncludes()
        {
            Query.Include(x => x.CommunityTags).ThenInclude(x => x.Tag);

            Query.Include(x => x.CommunitySocialPlatforms).ThenInclude(x => x.SocialPlatform);
        }
    }
}
