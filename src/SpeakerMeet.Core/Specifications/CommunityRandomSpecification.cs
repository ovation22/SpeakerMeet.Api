using System;
using Ardalis.Specification;
using SpeakerMeet.Core.Entities;

namespace SpeakerMeet.Core.Specifications
{
    public sealed class CommunityRandomSpecification : Specification<Community>
    {
        public CommunityRandomSpecification()
        {
            Query
                .Where(x => x.IsActive)
                .OrderBy(x => Guid.NewGuid());

            Query.Paginate(0, 4);
        }
    }
}