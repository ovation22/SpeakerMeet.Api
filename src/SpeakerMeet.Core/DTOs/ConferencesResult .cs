using System;
using System.Collections.Generic;

namespace SpeakerMeet.Core.DTOs
{
    public class ConferencesResult
    {
        public PaginationInfo PaginationInfo { get; set; } = default!;

        public IEnumerable<Conference> Conferences { get; set; } = new List<Conference>();

        public class Conference
        {
            public Guid Id { get; set; }

            public string Name { get; set; } = null!;

            public string Slug { get; set; } = null!;

            public string Location { get; set; } = null!;

            public string Description { get; set; } = null!;
        }
    }
}
