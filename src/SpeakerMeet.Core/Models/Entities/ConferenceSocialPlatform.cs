using System;
using System.ComponentModel.DataAnnotations;

namespace SpeakerMeet.Core.Models.Entities
{
    public class ConferenceSocialPlatform
    {
        [Key]
        public Guid Id { get; set; }
        
        public Guid ConferenceId { get; set; }

        public Conference Conference { get; set; } = default!;
        
        public Guid SocialPlatformId { get; set; }

        public SocialPlatform SocialPlatform { get; set; } = default!;

        public string Url { get; set; } = default!;
    }
}
