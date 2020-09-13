using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SpeakerMeet.Core.DTOs;

namespace SpeakerMeet.Core.Interfaces.Services
{
    public interface IConferenceService
    {
        Task<ConferenceResult> Get(Guid id);
        Task<ConferenceResult> Get(string slug);
        Task<ConferencesResult> GetAll(int pageIndex, int itemsPage);
        Task<IEnumerable<ConferenceFeatured>> GetFeatured();
    }
}
