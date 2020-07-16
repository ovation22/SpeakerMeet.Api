using Moq;
using SpeakerMeet.Core.Interfaces.Repositories;
using SpeakerMeet.Core.Services;

namespace SpeakerMeet.Core.Tests.Services.SpeakerServiceTests
{
    public class SpeakerServiceTestBase
    {
        protected internal SpeakerService Service;
        protected internal Mock<ISpeakerMeetRepository> Repository;

        public SpeakerServiceTestBase()
        {
            Repository = new Mock<ISpeakerMeetRepository>();

            Service = new SpeakerService(Repository.Object);
        }
    }
}
