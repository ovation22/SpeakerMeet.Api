using System;

namespace SpeakerMeet.Core.DTOs
{
    public class SpeakerPresentationsResult
    {
        public Guid Id { get; set; }

        public Guid SpeakerId { get; set; }

        public string Title { get; set; } = default!;

        public string Synopsis { get; set; } = default!;
    }
}
