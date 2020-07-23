using System;

namespace SpeakerMeet.Core.DTOs
{
    public class SpeakerResult
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string Slug { get; set; } = null!;

        public string Location { get; set; } = null!;

        public string Description { get; set; } = null!;
    }
}
