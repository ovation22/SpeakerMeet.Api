using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SpeakerMeet.Core.DTOs;

namespace SpeakerMeet.Core.Interfaces.Services
{
    public interface IConferenceService
    {
        Task<ConferenceResult> Get(Guid id);
        Task<IEnumerable<ConferencesResult>> GetAll();
    }
}
