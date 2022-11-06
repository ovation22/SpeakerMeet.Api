using System.Threading.Tasks;
using SpeakerMeet.Core.Models.DTOs;

namespace SpeakerMeet.Core.Interfaces.Services
{
    public interface ISearchService
    {
        Task<SpeakerMeetSearchResults> Search(string terms);
    }
}
