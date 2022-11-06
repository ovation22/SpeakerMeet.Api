using System;
using Ardalis.Specification;
using SpeakerMeet.Core.Models.Entities;

namespace SpeakerMeet.Core.Specifications;

public sealed class SpeakerRandomSpecification : Specification<Speaker>
{
    public SpeakerRandomSpecification()
    {
        Query
            .Where(x => x.IsActive)
            .AsNoTracking()
            .OrderBy(x => Guid.NewGuid());

        Query.Take(4);
    }
}