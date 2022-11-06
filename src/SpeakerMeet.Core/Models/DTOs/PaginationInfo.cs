namespace SpeakerMeet.Core.Models.DTOs
{
    public readonly record struct PaginationInfo(int TotalItems, int ItemsPerPage, int ActualPage, int TotalPages);
}
