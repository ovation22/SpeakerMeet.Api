using System;
using System.Collections.Generic;

namespace SpeakerMeet.Core.DTOs
{
    public record ConferencesResult
    {
        public PaginationInfo PaginationInfo { get; init; } = default!;

        public IEnumerable<Conference> Conferences { get; init; } = new List<Conference>();

        public record Conference
        {
            public Guid Id { get; init; }

            public string Name { get; init; } = null!;

            public string Slug { get; init; } = null!;

            public string Location { get; init; } = null!;

            public string Description { get; init; } = null!;
        }
    }
}
