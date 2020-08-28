using System;
using Ardalis.Specification;
using SpeakerMeet.Core.Entities;

namespace SpeakerMeet.Core.Specifications
{
    public class ConferenceSpecification : Specification<Conference>
    {
        public ConferenceSpecification(Guid id)
        {
            Query.Where(x => x.Id == id);
        }

        public ConferenceSpecification(string slug)
        {
            Query.Where(x => x.Slug == slug);
        }
    }
}