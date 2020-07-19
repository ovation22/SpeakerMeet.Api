using Moq;
using SpeakerMeet.Core.Interfaces.Repositories;
using SpeakerMeet.Core.Services;

namespace SpeakerMeet.Core.Tests.Services.ConferenceServiceTests
{
    public class ConferenceServiceTestBase
    {
        protected internal ConferenceService Service;
        protected internal Mock<ISpeakerMeetRepository> Repository;

        public ConferenceServiceTestBase()
        {
            Repository = new Mock<ISpeakerMeetRepository>();

            Service = new ConferenceService(Repository.Object);
        }
    }
}
