using System;
using System.Collections.Generic;

namespace SpeakerMeet.Core.DTOs
{
    public class CommunitiesResult
    {
        public PaginationInfo PaginationInfo { get; set; } = default!;

        public IEnumerable<Community> Communities { get; set; } = new List<Community>();

        public class Community
        {
            public Guid Id { get; set; }

            public string Name { get; set; } = null!;

            public string Slug { get; set; } = null!;

            public string Location { get; set; } = null!;

            public string Description { get; set; } = null!;

        }
    }
}
