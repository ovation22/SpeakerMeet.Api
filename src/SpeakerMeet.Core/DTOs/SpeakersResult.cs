using System;
using System.Collections.Generic;

namespace SpeakerMeet.Core.DTOs
{
    public record SpeakersResult
    {
        public PaginationInfo PaginationInfo { get; init; } = default!;

        public IEnumerable<Speaker> Speakers { get; init; } = new List<Speaker>();

        public record Speaker
        {
            public Guid Id { get; init; }

            public string Name { get; init; } = null!;

            public string Slug { get; init; } = null!;

            public string Description { get; init; } = null!;

            public string Location { get; init; } = null!;

        }
    }
}
