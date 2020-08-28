using System;
using Ardalis.Specification;
using SpeakerMeet.Core.Entities;

namespace SpeakerMeet.Core.Specifications
{
    public class CommunitySpecification : Specification<Community>
    {
        public CommunitySpecification(Guid id)
        {
            Query.Where(x => x.Id == id);
        }

        public CommunitySpecification(string slug)
        {
            Query.Where(x => x.Slug == slug);
        }
    }
}
