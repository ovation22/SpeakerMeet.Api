using System;
using Ardalis.Specification;
using SpeakerMeet.Core.Entities;

namespace SpeakerMeet.Core.Specifications
{
    public sealed class SpeakerPresentationSpecification : Specification<SpeakerPresentation>
    {
        public SpeakerPresentationSpecification(Guid speakerId)
        {
            Query.Where(x => x.SpeakerId == speakerId);
        }
    }
}
