using System.Threading.Tasks;
using SpeakerMeet.Core.Cache;
using SpeakerMeet.Core.DTOs;
using SpeakerMeet.Core.Entities;
using SpeakerMeet.Core.Interfaces.Caching;
using SpeakerMeet.Core.Interfaces.Repositories;
using SpeakerMeet.Core.Interfaces.Services;

namespace SpeakerMeet.Core.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly ICacheManager _cache;
        private readonly ISpeakerMeetRepository _repository;

        public StatisticsService(
            ICacheManager cache,
            ISpeakerMeetRepository repository
        )
        {
            _cache = cache;
            _repository = repository;
        }

        public async Task<CountResult> GetCounts()
        {
            return await _cache.GetOrCreate(CacheKeys.Counts, async () => await GetCountResult());
        }

        private async Task<CountResult> GetCountResult()
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