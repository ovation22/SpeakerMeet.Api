using System;
using Ardalis.Specification;
using SpeakerMeet.Core.Entities;

namespace SpeakerMeet.Core.Specifications
{
    public sealed class ConferenceRandomSpecification : Specification<Conference>
    {
        public ConferenceRandomSpecification()
        {
            Query
                .Where(x => x.IsActive)
                .OrderBy(x => Guid.NewGuid());

            Query.Take(4);
        }
    }
}