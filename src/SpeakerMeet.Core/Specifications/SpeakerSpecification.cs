using System;
using Ardalis.Specification;
using SpeakerMeet.Core.Entities;

namespace SpeakerMeet.Core.Specifications
{
    public class SpeakerSpecification : Specification<Speaker>
    {
        public SpeakerSpecification(Guid id)
        {
            Query.Where(x => x.Id == id);
        }

        public SpeakerSpecification(string slug)
        {
            Query.Where(x => x.Slug == slug);

            Query.Include(x => x.SpeakerTags).ThenInclude(x => x.Tag);

            Query.Include(x => x.SpeakerSocialPlatforms).ThenInclude(x => x.SocialPlatform);
        }
    }
}