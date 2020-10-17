using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SpeakerMeet.Core.Constants;
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
            await Controller.GetAll(0, 1, nameof(Direction.Asc));

            // Assert
            SpeakerService.Verify(x => x.GetAll(0, 1, nameof(Direction.Asc)), Times.Once());
        }

        [Fact]
        public async Task GivenException_ThenBadRequestResult()
        {
            // Arrange
            SpeakerService.Setup(x => x.GetAll(0, 1, null)).Throws(new Exception());

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
            SpeakerService.Setup(x => x.GetAll(0, 1, null)).Throws(ex);

            // Act
            await Controller.GetAll(0, 1);

            // Assert
            Logger.Verify(x => x.LogError(ex, ex.Message), Times.Once());
        }
    }
}
