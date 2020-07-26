using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace SpeakerMeet.Api.Tests.Controllers.SpeakersControllerTests
{
    public class GetFeatured : SpeakersControllerTestBase
    {
        [Fact]
        public async Task ItReturnsOkObjectResult()
        {
            // Arrange
            // Act
            var result = await Controller.GetFeatured();

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task ItGetsFeaturedSpeakers()
        {
            // Arrange
            // Act
            await Controller.GetFeatured();

            // Assert
            SpeakerService.Verify(x => x.GetFeatured(), Times.Once());
        }

        [Fact]
        public async Task GivenException_ThenBadRequestResult()
        {
            // Arrange
            SpeakerService.Setup(x => x.GetFeatured()).Throws(new Exception());

            // Act
            var result = await Controller.GetFeatured();

            // Assert
            Assert.IsAssignableFrom<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task GivenException_ThenItLogsError()
        {
            // Arrange
            var ex = new Exception();
            SpeakerService.Setup(x => x.GetFeatured()).Throws(ex);

            // Act
            await Controller.GetFeatured();

            // Assert
            Logger.Verify(x => x.LogError(ex, ex.Message), Times.Once());
        }
    }
}
