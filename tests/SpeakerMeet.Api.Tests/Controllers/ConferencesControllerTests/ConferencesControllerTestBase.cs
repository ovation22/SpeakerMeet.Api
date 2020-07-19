using Moq;
using SpeakerMeet.Api.Controllers;
using SpeakerMeet.Core.Interfaces.Logging;
using SpeakerMeet.Core.Interfaces.Services;

namespace SpeakerMeet.Api.Tests.Controllers.ConferencesControllerTests
{
    public class ConferencesControllerTestBase
    {
        protected internal ConferencesController Controller;
        protected internal Mock<IConferenceService> ConferenceService;
        protected internal Mock<ILoggerAdapter<ConferencesController>> Logger;

        public ConferencesControllerTestBase()
        {
            ConferenceService = new Mock<IConferenceService>();
            Logger = new Mock<ILoggerAdapter<ConferencesController>>();

            Controller = new ConferencesController(ConferenceService.Object, Logger.Object);
        }
    }
}
