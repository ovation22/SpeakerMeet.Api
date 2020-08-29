namespace SpeakerMeet.Core.DTOs
{
    public class PaginationInfo
    {
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int ActualPage { get; set; }
        public int TotalPages { get; set; }
        public string Previous { get; set; } = default!;
        public string Next { get; set; } = default!;
    }
}
