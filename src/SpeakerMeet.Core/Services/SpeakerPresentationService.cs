using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpeakerMeet.Core.DTOs;
using SpeakerMeet.Core.Interfaces.Repositories;
using SpeakerMeet.Core.Interfaces.Services;
using SpeakerMeet.Core.Specifications;

namespace SpeakerMeet.Core.Services
{
    public class SpeakerPresentationService : ISpeakerPresentationService
    {
        private readonly ISpeakerMeetRepository _repository;

        public SpeakerPresentationService(ISpeakerMeetRepository repository)
        {
            _repository = repository;
        }

        public async Task<IReadOnlyCollection<SpeakerPresentationsResult>> GetAll(Guid id)
        {
            var speakerPresentations = await _repository.List(new SpeakerPresentationSpecification(id));

            return speakerPresentations.Select(x => new SpeakerPresentationsResult
                {
                    Id = x.Id,
                    SpeakerId = x.SpeakerId,
                    Title = x.Title,
                    Synopsis = x.Synopsis
                })
                .ToList()
                .AsReadOnly();
        }
    }
}