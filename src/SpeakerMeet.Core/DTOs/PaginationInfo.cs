namespace SpeakerMeet.Core.DTOs
{
    public readonly record struct PaginationInfo(int TotalItems, int ItemsPerPage, int ActualPage, int TotalPages);
}
