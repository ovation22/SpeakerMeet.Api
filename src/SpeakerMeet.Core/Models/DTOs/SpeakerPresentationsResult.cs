using System;

namespace SpeakerMeet.Core.Models.DTOs
{
    public record SpeakerPresentationsResult
    {
        public Guid Id { get; init; }

        public Guid SpeakerId { get; init; }

        public string Title { get; init; } = default!;

        public string Synopsis { get; init; } = default!;
    }
}
