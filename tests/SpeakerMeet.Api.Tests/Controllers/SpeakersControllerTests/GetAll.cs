using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace SpeakerMeet.Api.Tests.Controllers.SpeakersControllerTests
{
    public class GetAll : SpeakersControllerTestBase
    {
        [Fact]
        public async Task ItReturnsOkObjectResult()
        {
            // Arrange
            // Act
            var result = await Controller.GetAll(0, 1);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task ItGetsSpeakers()
        {
            // Arrange
            // Act
            await Controller.GetAll(0, 1);

            // Assert
            SpeakerService.Verify(x => x.GetAll(0, 1), Times.Once());
        }

        [Fact]
        public async Task GivenException_ThenBadRequestResult()
        {
            // Arrange
            SpeakerService.Setup(x => x.GetAll(0, 1)).Throws(new Exception());

            // Act
            var result = await Controller.GetAll(0, 1);

            // Assert
            Assert.IsAssignableFrom<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task GivenException_ThenItLogsError()
        {
            // Arrange
            var ex = new Exception();
            SpeakerService.Setup(x => x.GetAll(0, 1)).Throws(ex);

            // Act
            await Controller.GetAll(0, 1);

            // Assert
            Logger.Verify(x => x.LogError(ex, ex.Message), Times.Once());
        }
    }
}
