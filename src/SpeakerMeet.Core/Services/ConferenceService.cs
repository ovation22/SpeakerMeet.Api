using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpeakerMeet.Core.DTOs;
using SpeakerMeet.Core.Entities;
using SpeakerMeet.Core.Interfaces.Repositories;
using SpeakerMeet.Core.Interfaces.Services;

namespace SpeakerMeet.Core.Services
{
    public class ConferenceService : IConferenceService
    {
        private readonly ISpeakerMeetRepository _repository;

        public ConferenceService(ISpeakerMeetRepository repository)
        {
            _repository = repository;
        }

        public async Task<ConferenceResult> Get(Guid id)
        {
            var conference =  await _repository.Get<Conference>(x => x.Id == id);

            return new ConferenceResult
            {
                Id = conference.Id,
                Location = conference.Location,
                Name = conference.Name,
                Slug = conference.Slug,
                Description = conference.Description
            };
        }

        public async Task<ConferenceResult> Get(string slug)
        {
            var conference =  await _repository.Get<Conference>(x => x.Slug == slug);

            return new ConferenceResult
            {
                Id = conference.Id,
                Location = conference.Location,
                Name = conference.Name,
                Slug = conference.Slug,
                Description = conference.Description
            };
        }

        public async Task<IEnumerable<ConferencesResult>> GetAll()
        {
            var conferences = await _repository.GetAll<Conference>();

            return conferences.Select(x => new ConferencesResult
            {
                Id = x.Id,
                Location = x.Location,
                Name = x.Name,
                Slug = x.Slug,
                Description = x.Description
            });
        }
    }
}