using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace SpeakerMeet.Api.Tests.Controllers.CommunitiesControllerTests
{
    public class GetFeatured : CommunitiesControllerTestBase
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
        public async Task ItGetsFeaturedCommunities()
        {
            // Arrange
            // Act
            await Controller.GetFeatured();

            // Assert
            CommunityService.Verify(x => x.GetFeatured(), Times.Once());
        }

        [Fact]
        public async Task GivenException_ThenBadRequestResult()
        {
            // Arrange
            CommunityService.Setup(x => x.GetFeatured()).Throws(new Exception());

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
            CommunityService.Setup(x => x.GetFeatured()).Throws(ex);

            // Act
            await Controller.GetFeatured();

            // Assert
            Logger.Verify(x => x.LogError(ex, ex.Message), Times.Once());
        }
    }
}
