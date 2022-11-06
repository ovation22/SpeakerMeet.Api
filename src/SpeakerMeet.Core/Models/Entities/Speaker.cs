using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SpeakerMeet.Core.Models.Entities
{
    public class Speaker
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; } = default!;

        public string Slug { get; set; } = default!;

        public string Location { get; set; } = default!;

        public string Description { get; set; } = default!;

        public bool IsActive { get; set; }

        public Guid? CreatedBy { get; set; }

        public DateTime Created { get; set; }

        public Guid? UpdatedBy { get; set; }

        public DateTime Updated { get; set; }

        public virtual ICollection<SpeakerTag> SpeakerTags { get; set; } = new List<SpeakerTag>();

        public virtual ICollection<SpeakerPresentation> SpeakerPresentation { get; set; } = new List<SpeakerPresentation>();

        public virtual ICollection<SpeakerSocialPlatform> SpeakerSocialPlatforms { get; set; } = new List<SpeakerSocialPlatform>();
    }
}
