using Moq;
using SpeakerMeet.Core.Interfaces.Repositories;
using SpeakerMeet.Core.Services;

namespace SpeakerMeet.Core.Tests.Services.StatisticsServiceTests
{
    public class StatisticsServiceTestBase
    {
        protected internal StatisticsService Service;
        protected internal Mock<ISpeakerMeetRepository> Repository;

        public StatisticsServiceTestBase()
        {
            Repository = new Mock<ISpeakerMeetRepository>();

            Service = new StatisticsService(Repository.Object);
        }
    }
}
