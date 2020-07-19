using Moq;
using SpeakerMeet.Api.Controllers;
using SpeakerMeet.Core.Interfaces.Logging;
using SpeakerMeet.Core.Interfaces.Services;

namespace SpeakerMeet.Api.Tests.Controllers.CommunitiesControllerTests
{
    public class CommunitiesControllerTestBase
    {
        protected internal CommunitiesController Controller;
        protected internal Mock<ICommunityService> CommunityService;
        protected internal Mock<ILoggerAdapter<CommunitiesController>> Logger;

        public CommunitiesControllerTestBase()
        {
            CommunityService = new Mock<ICommunityService>();
            Logger = new Mock<ILoggerAdapter<CommunitiesController>>();

            Controller = new CommunitiesController(CommunityService.Object, Logger.Object);
        }
    }
}
