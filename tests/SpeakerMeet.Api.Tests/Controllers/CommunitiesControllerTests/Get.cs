using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace SpeakerMeet.Api.Tests.Controllers.CommunitiesControllerTests
{
    public class Get : CommunitiesControllerTestBase
    {
        private readonly Guid _id;

        public Get()
        {
            _id = new Guid("00CC2531-5549-4EB7-B233-B573A7655413");
        }

        [Fact]
        public async Task ItReturnsOkObjectResult()
        {
            // Arrange
            // Act
            var result = await Controller.Get(_id);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task ItGetsCommunity()
        {
            // Arrange
            // Act
            await Controller.Get(_id);

            // Assert
            CommunityService.Verify(x => x.Get(_id), Times.Once());
        }

        [Fact]
        public async Task GivenException_ThenBadRequestResult()
        {
            // Arrange
            CommunityService.Setup(x => x.Get(_id)).Throws(new Exception());

            // Act
            var result = await Controller.Get(_id);

            // Assert
            Assert.IsAssignableFrom<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task GivenException_ThenItLogsError()
        {
            // Arrange
            var ex = new Exception();
            CommunityService.Setup(x => x.Get(_id)).Throws(ex);

            // Act
            await Controller.Get(_id);

            // Assert
            Logger.Verify(x => x.LogError(ex, ex.Message), Times.Once());
        }
    }
}
