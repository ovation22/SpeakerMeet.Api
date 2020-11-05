using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace SpeakerMeet.Api.Tests.Controllers.ConferencesControllerTests
{
    public class GetFeatured : ConferencesControllerTestBase
    {
        [Fact]
        public async Task ItReturnsOkObjectResult()
        {
            // Arrange
            // Act
            var result = await Controller.GetFeatured();

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task ItGetsFeaturedConferences()
        {
            // Arrange
            // Act
            await Controller.GetFeatured();

            // Assert
            ConferenceService.Verify(x => x.GetFeatured(), Times.Once());
        }

        [Fact]
        public async Task GivenException_ThenBadRequestResult()
        {
            // Arrange
            ConferenceService.Setup(x => x.GetFeatured()).Throws(new Exception());

            // Act
            var result = await Controller.GetFeatured();

            // Assert
            Assert.IsAssignableFrom<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public async Task GivenException_ThenItLogsError()
        {
            // Arrange
            var ex = new Exception();
            ConferenceService.Setup(x => x.GetFeatured()).Throws(ex);

            // Act
            await Controller.GetFeatured();

            // Assert
            Logger.Verify(x => x.LogError(ex, ex.Message), Times.Once());
        }
    }
}
