using Moq;
using SpeakerMeet.Core.Interfaces.Repositories;
using SpeakerMeet.Core.Services;

namespace SpeakerMeet.Core.Tests.Services.CommunityServiceTests
{
    public class CommunityServiceTestBase
    {
        protected internal CommunityService Service;
        protected internal Mock<ISpeakerMeetRepository> Repository;

        public CommunityServiceTestBase()
        {
            Repository = new Mock<ISpeakerMeetRepository>();

            Service = new CommunityService(Repository.Object);
        }
    }
}
