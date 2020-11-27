using System;
using System.Collections.Generic;

namespace SpeakerMeet.Core.DTOs
{
    public record CommunitiesResult
    {
        public PaginationInfo PaginationInfo { get; init; } = default!;

        public IEnumerable<Community> Communities { get; set; } = new List<Community>();

        public record Community
        {
            public Guid Id { get; init; }

            public string Name { get; init; } = null!;

            public string Slug { get; init; } = null!;

            public string Location { get; init; } = null!;

            public string Description { get; init; } = null!;
        }
    }
}
