using System;
using System.ComponentModel.DataAnnotations;

namespace SpeakerMeet.Core.Entities
{
    public class SpeakerPresentation
    {
        [Key]
        public Guid Id { get; set; }
        
        public Guid SpeakerId { get; set; }

        public Speaker Speaker { get; set; } = default!;

        public string Title { get; set; } = default!;

        public string Synopsis { get; set; } = default!;
    }
}
