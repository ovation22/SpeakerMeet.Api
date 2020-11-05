using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SpeakerMeet.Core.Constants;
using Xunit;

namespace SpeakerMeet.Api.Tests.Controllers.CommunitiesControllerTests
{
    public class GetAll : CommunitiesControllerTestBase
    {
        [Fact]
        public async Task ItReturnsOkObjectResult()
        {
            // Arrange
            // Act
            var result = await Controller.GetAll(0, 1);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task ItGetsCommunities()
        {
            // Arrange
            // Act
            await Controller.GetAll(0, 1, nameof(Direction.Asc));

            // Assert
            CommunityService.Verify(x => x.GetAll(0, 1, nameof(Direction.Asc)), Times.Once());
        }

        [Fact]
        public async Task GivenException_ThenBadRequestResult()
        {
            // Arrange
            CommunityService.Setup(x => x.GetAll(0, 1, nameof(Direction.Desc))).Throws(new Exception());

            // Act
            var result = await Controller.GetAll(0, 1, nameof(Direction.Desc));

            // Assert
            Assert.IsAssignableFrom<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public async Task GivenException_ThenItLogsError()
        {
            // Arrange
            var ex = new Exception();
            CommunityService.Setup(x => x.GetAll(0, 1, nameof(Direction.Asc))).Throws(ex);

            // Act
            await Controller.GetAll(0, 1, nameof(Direction.Asc));

            // Assert
            Logger.Verify(x => x.LogError(ex, ex.Message), Times.Once());
        }
    }
}