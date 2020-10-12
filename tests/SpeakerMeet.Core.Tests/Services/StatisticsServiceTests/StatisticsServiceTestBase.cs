using Moq;
using SpeakerMeet.Core.Interfaces.Caching;
using SpeakerMeet.Core.Interfaces.Repositories;
using SpeakerMeet.Core.Services;

namespace SpeakerMeet.Core.Tests.Services.StatisticsServiceTests
{
    public class StatisticsServiceTestBase
    {
        protected internal StatisticsService Service;
        protected internal Mock<ICacheManager> Cache;
        protected internal Mock<ISpeakerMeetRepository> Repository;

        public StatisticsServiceTestBase()
        {
            Cache = new Mock<ICacheManager>();
            Repository = new Mock<ISpeakerMeetRepository>();

            Service = new StatisticsService(Cache.Object, Repository.Object);
        }
    }
}
