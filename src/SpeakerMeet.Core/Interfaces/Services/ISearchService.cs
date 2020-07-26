using System.Threading.Tasks;
using Microsoft.Azure.Search.Models;
using SpeakerMeet.Core.DTOs;

namespace SpeakerMeet.Core.Interfaces.Services
{
    public interface ISearchService
    {
        Task<DocumentSearchResult<SearchResults>> Search(string terms);
    }
}
