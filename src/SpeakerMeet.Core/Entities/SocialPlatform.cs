using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SpeakerMeet.Core.Entities
{
    public class SocialPlatform
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public IEnumerable<SpeakerSocialPlatform>? SpeakerSocialPlatforms { get; set; }
    }
}
