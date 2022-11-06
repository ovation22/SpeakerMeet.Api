using System;
using System.ComponentModel.DataAnnotations;

namespace SpeakerMeet.Core.Models.Entities
{
    public class CommunityTag
    {
        [Key]
        public Guid Id { get; set; }
        
        public Guid CommunityId { get; set; }

        public Community Community { get; set; } = null!;
        
        public Guid TagId { get; set; }

        public Tag Tag { get; set; } = null!;
    }
}
