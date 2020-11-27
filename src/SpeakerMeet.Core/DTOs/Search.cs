using System.Collections.Generic;
using Microsoft.Azure.Search.Models;

namespace SpeakerMeet.Core.DTOs
{
    public record Search
    {
        public int Page { get; init; } = 1;
        public int PageSize { get; init; } = 10;
        public bool IncludeFacets { get; init; } = false;
        public string Text { get; init; } = default!;
        public Dictionary<string, string> Filters { get; init; } = new Dictionary<string, string>();
        public List<string> Facets { get; init; } = new List<string>();
        public string OrderBy { get; init; } = "";
        public string QueryType { get; init; } = "simple";
        public SearchMode SearchMode { get; init; } = SearchMode.Any;
        public string ScoringProfile { get; init; } = default!;
    }
}
