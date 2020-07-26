using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using SpeakerMeet.Core.DTOs;
using SpeakerMeet.Core.Interfaces.Services;

namespace SpeakerMeet.Core.Services
{
    public class SearchService : ISearchService
    {
        private readonly string _searchIndexName;
        private readonly SearchServiceClient _client;
        private readonly ConcurrentDictionary<string, ISearchIndexClient> _indexClients;

        public SearchService(string accountName, string queryKey, string searchIndexName)
        {
            _searchIndexName = searchIndexName;
            _indexClients = new ConcurrentDictionary<string, ISearchIndexClient>();
            _client = new SearchServiceClient(accountName, new SearchCredentials(queryKey));
        }

        private ISearchIndexClient GetClient(string indexName)
        {
            return _indexClients.GetOrAdd(indexName, _client.Indexes.GetClient(indexName));
        }

        public async Task<DocumentSearchResult<SearchResults>> Search(string terms)
        {
            var payload = new Search
            {
                Text = terms.Replace(" ", "~ "),
                QueryType = "full"
            };

            return await Search(_searchIndexName, payload);
        }

        public async Task<DocumentSearchResult<SearchResults>> Search(string indexName, Search payload)
        {
            var indexClient = GetClient(indexName);
            var searchParameters = new SearchParameters {Top = payload.PageSize, Skip = (payload.Page - 1) * payload.PageSize};
            
            if (payload.Filters != null)
            {
                searchParameters.Filter = string.Join(" and ", payload.Filters.Select(x => $"{x.Key} eq '{x.Value}'").ToArray());
            }
            
            searchParameters.OrderBy = payload.OrderBy.Split(',');
            searchParameters.QueryType = "full".Equals(payload.QueryType) ? QueryType.Full : QueryType.Simple;
            searchParameters.SearchMode = payload.SearchMode;

            if (!string.IsNullOrEmpty(payload.ScoringProfile))
            {
                searchParameters.ScoringProfile = payload.ScoringProfile;
            }
            searchParameters.IncludeTotalResultCount = true;
            
            if (payload.IncludeFacets)
            {
                var appliedFilters = (payload.Filters ?? new Dictionary<string, string>()).Select(x => x.Key);
                searchParameters.Facets = payload.Facets.Except(appliedFilters).ToList();
            }

            return await indexClient.Documents.SearchAsync<SearchResults>(payload.Text, searchParameters);
        }
    }
}
