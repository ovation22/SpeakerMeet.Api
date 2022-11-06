using System;
using Ardalis.Specification;
using SpeakerMeet.Core.Models.Entities;

namespace SpeakerMeet.Core.Specifications;

public sealed class CommunityRandomSpecification : Specification<Community>
{
    public CommunityRandomSpecification()
    {
        Query
            .Where(x => x.IsActive)
            .AsNoTracking()
            .OrderBy(x => Guid.NewGuid());

        Query.Take(4);
    }
}