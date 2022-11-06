using System;
using System.Linq;
using System.Threading.Tasks;
using Azure;
using Azure.Search.Documents;
using Azure.Search.Documents.Models;
using SpeakerMeet.Core.Interfaces.Services;
using SpeakerMeet.Core.Models.DTOs;

namespace SpeakerMeet.Core.Services
{
    public class SearchService : ISearchService
    {
        private readonly SearchClient _client;

        public SearchService(string accountName, string queryKey, string searchIndexName)
        {
            _client = new SearchClient(new Uri($"https://{accountName}.search.windows.net"), 
                searchIndexName, new AzureKeyCredential(queryKey));
        }

        public async Task<SpeakerMeetSearchResults> Search(string terms)
        {
            var fuzzyTerms = terms + "~";
            var splitTerms = terms.Replace(" ", "~ ");

            var payload = new Search
            {
                Text = $"{terms} OR {fuzzyTerms} OR {splitTerms}",
                QueryType = "full"
            };

            return await Search(payload);
        }

        private async Task<SpeakerMeetSearchResults> Search(Search payload)
        {
            var searchOptions =
                new SearchOptions
                {
                    Size = payload.PageSize,
                    Skip = (payload.Page - 1) * payload.PageSize,
                    QueryType = "full".Equals(payload.QueryType) ? SearchQueryType.Full : SearchQueryType.Simple,
                    SearchMode = payload.SearchMode,
                    IncludeTotalCount = true
                };

            ApplyFilter(payload, searchOptions);
            ApplyFacets(payload, searchOptions);
            ApplyOrderBy(payload, searchOptions);
            ApplyScoringProfile(payload, searchOptions);

            SearchResults<SpeakerMeetSearchResult> results = await _client.SearchAsync<SpeakerMeetSearchResult>(payload.Text, searchOptions);

            return new SpeakerMeetSearchResults
            {
                Count = results.TotalCount,
                Results = results.GetResults().Select(x => new SpeakerMeetSearchResultDocument
                {
                    Document =
                        new SpeakerMeetSearchResult
                        {
                            Id = x.Document.Id,
                            Location = x.Document.Location,
                            Slug = x.Document.Slug,
                            Name = x.Document.Name,
                            Type = x.Document.Type,
                            Description = x.Document.Description
                        }
                })
            };
        }

        private static void ApplyFacets(Search payload, SearchOptions searchOptions)
        {
            if (!payload.IncludeFacets)
            {
                return;
            }

            var appliedFacets = (payload.Filters).Select(x => x.Key);

            foreach (var facet in payload.Facets.Except(appliedFacets).ToList())
            {
                searchOptions.Facets.Add(facet);
            }
        }

        private static void ApplyScoringProfile(Search payload, SearchOptions searchOptions)
        {
            if (!string.IsNullOrEmpty(payload.ScoringProfile))
            {
                searchOptions.ScoringProfile = payload.ScoringProfile;
            }
        }

        private static void ApplyOrderBy(Search payload, SearchOptions searchOptions)
        {
            foreach (var orderBy in payload.OrderBy.Split(','))
            {
                searchOptions.OrderBy.Add(orderBy);
            }
        }

        private static void ApplyFilter(Search payload, SearchOptions searchOptions)
        {
            if (payload.Filters.Count > 0)
            {
                searchOptions.Filter = string.Join(" and ", payload.Filters.Select(x => $"{x.Key} eq '{x.Value}'").ToArray());
            }
        }
    }
}
