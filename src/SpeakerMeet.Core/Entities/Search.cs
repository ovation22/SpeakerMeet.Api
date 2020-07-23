using System;

namespace SpeakerMeet.Core.Entities
{
    public class Search
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string Slug { get; set; } = null!;

        public string Location { get; set; } = null!;

        public string Description { get; set; } = null!;

        public bool IsActive { get; set; }

        public DateTime Created { get; set; }

        public DateTime Updated { get; set; }

        public string Type { get; set; } = null!;

        public string Tags { get; set; } = null!;
    }
}
