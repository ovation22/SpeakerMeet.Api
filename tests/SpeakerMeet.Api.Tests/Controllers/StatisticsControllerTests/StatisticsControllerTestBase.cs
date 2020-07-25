using Moq;
using SpeakerMeet.Api.Controllers;
using SpeakerMeet.Core.Interfaces.Logging;
using SpeakerMeet.Core.Interfaces.Services;

namespace SpeakerMeet.Api.Tests.Controllers.StatisticsControllerTests
{
    public class StatisticsControllerTestBase
    {
        protected internal StatisticsController Controller;
        protected internal Mock<IStatisticsService> StatisticsService;
        protected internal Mock<ILoggerAdapter<StatisticsController>> Logger;

        public StatisticsControllerTestBase()
        {
            StatisticsService = new Mock<IStatisticsService>();
            Logger = new Mock<ILoggerAdapter<StatisticsController>>();

            Controller = new StatisticsController(StatisticsService.Object, Logger.Object);
        }
    }
}
