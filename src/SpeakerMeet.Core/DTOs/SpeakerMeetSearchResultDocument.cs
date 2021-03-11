namespace SpeakerMeet.Core.DTOs
{
    public record SpeakerMeetSearchResultDocument
    {
        public SpeakerMeetSearchResult Document { get; init; } = default!;
    }
}