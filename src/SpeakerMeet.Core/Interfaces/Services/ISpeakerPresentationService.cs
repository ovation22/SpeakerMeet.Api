using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SpeakerMeet.Core.Models.DTOs;

namespace SpeakerMeet.Core.Interfaces.Services
{
    public interface ISpeakerPresentationService
    {
        Task<IReadOnlyCollection<SpeakerPresentationsResult>> GetAll(Guid id);
    }
}
