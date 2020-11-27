namespace SpeakerMeet.Core.DTOs
{
    public record SocialMedia
    {
        public string Name { get; init; } = default!;

        public string Url { get; init; } = default!;
    }
}
