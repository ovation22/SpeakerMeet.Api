using System;
using System.Collections.Generic;

namespace SpeakerMeet.Core.DTOs
{
    public record SpeakerResult
    {
        public Guid Id { get; init; }

        public string Name { get; init; } = null!;

        public string Slug { get; init; } = null!;

        public string Location { get; init; } = null!;

        public string Description { get; init; } = null!;

        public IEnumerable<string>? Tags { get; init; }

        public IEnumerable<SocialMedia>? SocialPlatforms { get; init; }
    }
}
