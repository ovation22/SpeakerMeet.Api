using System;
using System.Collections.Generic;

namespace SpeakerMeet.Core.DTOs
{
    public class SpeakersResult
    {
        public PaginationInfo PaginationInfo { get; set; } = default!;

        public IEnumerable<Speaker> Speakers { get; set; } = new List<Speaker>();

        public class Speaker
        {
            public Guid Id { get; set; }

            public string Name { get; set; } = null!;

            public string Slug { get; set; } = null!;

            public string Description { get; set; } = null!;

            public string Location { get; set; } = null!;

        }
    }
}
