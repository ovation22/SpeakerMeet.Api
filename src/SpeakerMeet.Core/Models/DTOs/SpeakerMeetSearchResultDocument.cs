namespace SpeakerMeet.Core.Models.DTOs
{
    public record SpeakerMeetSearchResultDocument
    {
        public SpeakerMeetSearchResult Document { get; init; } = default!;
    }
}