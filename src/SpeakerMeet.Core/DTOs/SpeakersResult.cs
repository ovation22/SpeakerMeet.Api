using System;
using System.Collections.Generic;

namespace SpeakerMeet.Core.DTOs
{
    public record SpeakersResult
    {
        public PaginationInfo PaginationInfo { get; init; } = default!;

        public IReadOnlyCollection<Speaker> Speakers { get; init; } = Array.Empty<Speaker>();

        public readonly record struct Speaker
        {
            public Guid Id { get; init; }

            public string Name { get; init; } = null!;

            public string Slug { get; init; } = null!;

            public string Description { get; init; } = null!;

            public string Location { get; init; } = null!;

        }
    }
}
