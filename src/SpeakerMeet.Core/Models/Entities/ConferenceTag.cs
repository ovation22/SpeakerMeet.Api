using System;
using System.ComponentModel.DataAnnotations;

namespace SpeakerMeet.Core.Models.Entities
{
    public class ConferenceTag
    {
        [Key]
        public Guid Id { get; set; }
        
        public Guid ConferenceId { get; set; }

        public Conference Conference { get; set; } = null!;
        
        public Guid TagId { get; set; }

        public Tag Tag { get; set; } = null!;
    }
}
