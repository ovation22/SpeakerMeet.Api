﻿using System.Collections.Generic;
using Microsoft.Azure.Search.Models;

namespace SpeakerMeet.Core.DTOs
{
    public class Search
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public bool IncludeFacets { get; set; } = false;
        public string Text { get; set; } = default!;
        public Dictionary<string, string> Filters { get; set; } = new Dictionary<string, string>();
        public List<string> Facets { get; set; } = new List<string>();
        public string OrderBy { get; set; } = "";
        public string QueryType { get; set; } = "simple";
        public SearchMode SearchMode { get; set; } = SearchMode.Any;
        public string ScoringProfile { get; set; } = default!;
    }
}
