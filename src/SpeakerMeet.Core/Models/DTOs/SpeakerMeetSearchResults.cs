using System.Collections.Generic;

namespace SpeakerMeet.Core.Models.DTOs
{
    public record SpeakerMeetSearchResults
    {
        public long? Count { get; init; }
        public IEnumerable<SpeakerMeetSearchResultDocument> Results { get; init; } = default!;
    }
}