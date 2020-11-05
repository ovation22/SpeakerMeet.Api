using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace SpeakerMeet.Api.Tests.Controllers.StatisticsControllerTests
{
    public class GetCounts : StatisticsControllerTestBase
    {
        [Fact]
        public async Task ItReturnsOkObjectResult()
        {
            // Arrange
            // Act
            var result = await Controller.GetCounts();

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task ItGetsStatistics()
        {
            // Arrange
            // Act
            await Controller.GetCounts();

            // Assert
            StatisticsService.Verify(x => x.GetCounts(), Times.Once());
        }

        [Fact]
        public async Task GivenException_ThenBadRequestResult()
        {
            // Arrange
            StatisticsService.Setup(x => x.GetCounts()).Throws(new Exception());

            // Act
            var result = await Controller.GetCounts();

            // Assert
            Assert.IsAssignableFrom<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public async Task GivenException_ThenItLogsError()
        {
            // Arrange
            var ex = new Exception();
            StatisticsService.Setup(x => x.GetCounts()).Throws(ex);

            // Act
            await Controller.GetCounts();

            // Assert
            Logger.Verify(x => x.LogError(ex, ex.Message), Times.Once());
        }
    }
}
