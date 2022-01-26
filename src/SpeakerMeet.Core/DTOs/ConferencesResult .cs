using System;
using System.Collections.Generic;

namespace SpeakerMeet.Core.DTOs
{
    public record ConferencesResult
    {
        public PaginationInfo PaginationInfo { get; init; } = default!;

        public IReadOnlyCollection<Conference> Conferences { get; init; } = Array.Empty<Conference>();

        public readonly record struct Conference
        {
            public Guid Id { get; init; }

            public string Name { get; init; } = null!;

            public string Slug { get; init; } = null!;

            public string Location { get; init; } = null!;

            public string Description { get; init; } = null!;
        }
    }
}
