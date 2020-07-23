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
    public class SpeakerService : ISpeakerService
    {
        private readonly ISpeakerMeetRepository _repository;

        public SpeakerService(ISpeakerMeetRepository repository)
        {
            _repository = repository;
        }

        public async Task<SpeakerResult> Get(Guid id)
        {
            var speaker =  await _repository.Get<Speaker>(x => x.Id == id);

            return new SpeakerResult
            {
                Id = speaker.Id,
                Location = speaker.Location,
                Name = speaker.Name,
                Slug = speaker.Slug,
                Description = speaker.Description
            };
        }

        public async Task<SpeakerResult> Get(string slug)
        {
            var speaker =  await _repository.Get<Speaker>(x => x.Slug == slug);

            return new SpeakerResult
            {
                Id = speaker.Id,
                Location = speaker.Location,
                Name = speaker.Name,
                Slug = speaker.Slug,
                Description = speaker.Description
            };
        }

        public async Task<IEnumerable<SpeakersResult>> GetAll()
        {
            var speakers = await _repository.GetAll<Speaker>();

            return speakers.Select(x => new SpeakersResult
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