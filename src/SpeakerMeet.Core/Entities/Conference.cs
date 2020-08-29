using System;
using System.Collections.Generic;
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

        public string Description { get; set; } = null!;

        public bool IsActive { get; set; }

        public Guid? CreatedBy { get; set; }

        public DateTime Created { get; set; }

        public Guid? UpdatedBy { get; set; }

        public DateTime Updated { get; set; }

        public virtual ICollection<ConferenceTag> ConferenceTags { get; set; } = new List<ConferenceTag>();

        public virtual ICollection<ConferenceSocialPlatform> ConferenceSocialPlatforms { get; set; } = new List<ConferenceSocialPlatform>();
    }
}
