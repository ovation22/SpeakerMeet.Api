using System;
using Ardalis.Specification;
using SpeakerMeet.Core.Entities;

namespace SpeakerMeet.Core.Specifications
{
    public class ConferenceRandomSpecification : Specification<Conference>
    {
        public ConferenceRandomSpecification()
        {
            Take = 4;
            Query
                .OrderBy(x => Guid.NewGuid());
        }

        public new int Take { get; internal set; }
    }
}