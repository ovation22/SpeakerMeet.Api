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
        protected internal Mock<ISpeakerPresentationService> SpeakerPresentationService;

        public SpeakersControllerTestBase()
        {
            SpeakerService = new Mock<ISpeakerService>();
            Logger = new Mock<ILoggerAdapter<SpeakersController>>();
            SpeakerPresentationService = new Mock<ISpeakerPresentationService>();

            Controller = new SpeakersController(SpeakerService.Object, Logger.Object, SpeakerPresentationService.Object);
        }
    }
}
