using System;
using System.ComponentModel.DataAnnotations;

namespace SpeakerMeet.Core.Entities
{
    public class Conference
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string Slug { get; set; } = null!;

        public string Location { get; set; } = null!;

        public bool IsActive { get; set; }

        public Guid CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public Guid UpdatedBy { get; set; }

        public DateTime UpdatedOn { get; set; }
    }
}
