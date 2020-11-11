using System.Threading.Tasks;
using SpeakerMeet.Core.DTOs;
using Microsoft.Azure.Search.Models;

namespace SpeakerMeet.Core.Interfaces.Services
{
    public interface ISearchService
    {
        Task<DocumentSearchResult<SearchResults>> Search(string terms);
    }
}
