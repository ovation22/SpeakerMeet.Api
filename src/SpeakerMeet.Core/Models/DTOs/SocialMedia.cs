namespace SpeakerMeet.Core.Models.DTOs
{
    public record SocialMedia
    {
        public string Name { get; init; } = default!;

        public string Url { get; init; } = default!;
    }
}
