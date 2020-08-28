using System;
using System.ComponentModel.DataAnnotations;

namespace SpeakerMeet.Core.Entities
{
    public class SpeakerSocialPlatform
    {
        [Key]
        public Guid Id { get; set; }
        
        public Guid SpeakerId { get; set; }

        public Speaker Speaker { get; set; } = default!;
        
        public Guid SocialPlatformId { get; set; }

        public SocialPlatform SocialPlatform { get; set; } = default!;

        public string Url { get; set; } = default!;
    }
}
