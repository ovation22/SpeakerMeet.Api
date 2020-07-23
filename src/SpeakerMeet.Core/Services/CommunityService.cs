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
    public class CommunityService : ICommunityService
    {
        private readonly ISpeakerMeetRepository _repository;

        public CommunityService(ISpeakerMeetRepository repository)
        {
            _repository = repository;
        }

        public async Task<CommunityResult> Get(Guid id)
        {
            var community =  await _repository.Get<Community>(x => x.Id == id);

            return new CommunityResult
            {
                Id = community.Id,
                Location = community.Location,
                Name = community.Name,
                Slug = community.Slug,
                Description = community.Description
            };
        }

        public async Task<CommunityResult> Get(string slug)
        {
            var community =  await _repository.Get<Community>(x => x.Slug == slug);

            return new CommunityResult
            {
                Id = community.Id,
                Location = community.Location,
                Name = community.Name,
                Slug = community.Slug,
                Description = community.Description
            };
        }

        public async Task<IEnumerable<CommunitiesResult>> GetAll()
        {
            var communities = await _repository.GetAll<Community>();

            return communities.Select(x => new CommunitiesResult
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