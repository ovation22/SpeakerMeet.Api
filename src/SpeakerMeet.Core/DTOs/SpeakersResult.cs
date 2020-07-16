using System;

namespace SpeakerMeet.Core.DTOs
{
    public class SpeakersResult
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string Slug { get; set; } = null!;

        public string Location { get; set; } = null!;
    }
}
