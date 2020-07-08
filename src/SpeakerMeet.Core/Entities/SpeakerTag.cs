using System;
using System.ComponentModel.DataAnnotations;

namespace SpeakerMeet.Core.Entities
{
    public class SpeakerTag
    {
        [Key]
        public Guid Id { get; set; }
        
        public Guid SpeakerId { get; set; }

        public Speaker Speaker { get; set; } = null!;
        
        public Guid TagId { get; set; }

        public Tag Tag { get; set; } = null!;
    }
}
