using System;
using Ardalis.Specification;
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
            Query.Where(x => x.Slug == slug);

            WithIncludes();
        }

        private void WithIncludes()
        {
            Query.Include(x => x.ConferenceTags).ThenInclude(x => x.Tag);

            Query.Include(x => x.ConferenceSocialPlatforms).ThenInclude(x => x.SocialPlatform);
        }
    }
}