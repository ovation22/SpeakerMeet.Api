using System;
using System.Collections.Generic;

namespace SpeakerMeet.Core.DTOs
{
    public record CommunitiesResult
    {
        public PaginationInfo PaginationInfo { get; init; } = default!;

        public IReadOnlyCollection<Community> Communities { get; set; } = Array.Empty<Community>();

        public readonly record struct Community
        {
            public Guid Id { get; init; }

            public string Name { get; init; } = null!;

            public string Slug { get; init; } = null!;

            public string Location { get; init; } = null!;

            public string Description { get; init; } = null!;
        }
    }
}
