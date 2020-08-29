using System;
using System.ComponentModel.DataAnnotations;

namespace SpeakerMeet.Core.Entities
{
    public class CommunitySocialPlatform
    {
        [Key]
        public Guid Id { get; set; }
        
        public Guid CommunityId { get; set; }

        public Community Community { get; set; } = default!;
        
        public Guid SocialPlatformId { get; set; }

        public SocialPlatform SocialPlatform { get; set; } = default!;

        public string Url { get; set; } = default!;
    }
}
