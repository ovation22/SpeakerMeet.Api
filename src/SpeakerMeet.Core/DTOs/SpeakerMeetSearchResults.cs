using System.Collections.Generic;

namespace SpeakerMeet.Core.DTOs
{
    public record SpeakerMeetSearchResults
    {
        public long? Count { get; init; }
        public IEnumerable<SpeakerMeetSearchResultDocument> Results { get; init; } = default!;
    }
}