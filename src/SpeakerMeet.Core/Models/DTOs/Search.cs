using System.Collections.Generic;
using Azure.Search.Documents.Models;

namespace SpeakerMeet.Core.Models.DTOs
{
    public record Search
    {
        public int Page { get; init; } = 1;
        public int PageSize { get; init; } = 10;
        public bool IncludeFacets { get; init; } = false;
        public string Text { get; init; } = default!;
        public Dictionary<string, string> Filters { get; init; } = new();
        public List<string> Facets { get; init; } = new();
        public string OrderBy { get; init; } = "";
        public string QueryType { get; init; } = "simple";
        public SearchMode SearchMode { get; init; } = SearchMode.Any;
        public string ScoringProfile { get; init; } = default!;
    }
}
