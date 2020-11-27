namespace SpeakerMeet.Core.DTOs
{
    public record PaginationInfo
    {
        public int TotalItems { get; init; }
        public int ItemsPerPage { get; init; }
        public int ActualPage { get; init; }
        public int TotalPages { get; init; }
    }
}
