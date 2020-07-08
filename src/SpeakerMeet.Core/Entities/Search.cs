using System;
using System.ComponentModel.DataAnnotations;

namespace SpeakerMeet.Core.Entities
{
    public class Search
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string Slug { get; set; } = null!;

        public string Location { get; set; } = null!;

        public bool IsActive { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime UpdatedOn { get; set; }

        public string Type { get; set; } = null!;

        public string Tags { get; set; } = null!;
    }
}
