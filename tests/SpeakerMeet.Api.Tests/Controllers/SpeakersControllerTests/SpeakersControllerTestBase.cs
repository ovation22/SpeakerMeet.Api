using Moq;
using SpeakerMeet.Api.Controllers;
using SpeakerMeet.Core.Interfaces.Logging;
using SpeakerMeet.Core.Interfaces.Services;

namespace SpeakerMeet.Api.Tests.Controllers.SpeakersControllerTests
{
    public class SpeakersControllerTestBase
    {
        protected internal SpeakersController Controller;
        protected internal Mock<ISpeakerService> SpeakerService;
        protected internal Mock<ILoggerAdapter<SpeakersController>> Logger;

        public SpeakersControllerTestBase()
        {
            SpeakerService = new Mock<ISpeakerService>();
            Logger = new Mock<ILoggerAdapter<SpeakersController>>();

            Controller = new SpeakersController(SpeakerService.Object, Logger.Object);
        }
    }
}
