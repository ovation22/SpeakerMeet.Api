using System.Threading.Tasks;
using SpeakerMeet.Core.DTOs;
using SpeakerMeet.Core.Entities;
using SpeakerMeet.Core.Interfaces.Repositories;
using SpeakerMeet.Core.Interfaces.Services;

namespace SpeakerMeet.Core.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly ISpeakerMeetRepository _repository;

        public StatisticsService(ISpeakerMeetRepository repository)
        {
            _repository = repository;
        }

        public async Task<CountResult> GetCounts()
        {
            var speakers = await _repository.Count<Speaker>();
            var communities = await _repository.Count<Community>();
            var conferences = await _repository.Count<Conference>();

            return new CountResult
            {
                SpeakerCount = speakers,
                CommunityCount = communities,
                ConferenceCount = conferences
            };
        }
    }
}