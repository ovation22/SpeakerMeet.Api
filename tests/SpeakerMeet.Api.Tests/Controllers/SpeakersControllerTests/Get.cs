using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace SpeakerMeet.Api.Tests.Controllers.SpeakersControllerTests
{
    public class Counts : SpeakersControllerTestBase
    {
        private readonly Guid _id;

        public Counts()
        {
            _id = new Guid("170E1003-5346-4E57-8721-FBBDFBC28EF6");
        }

        [Fact]
        public async Task ItReturnsOkObjectResult()
        {
            // Arrange
            // Act
            var result = await Controller.Get(_id);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task ItGetsSpeaker()
        {
            // Arrange
            // Act
            await Controller.Get(_id);

            // Assert
            SpeakerService.Verify(x => x.Get(_id), Times.Once());
        }

        [Fact]
        public async Task GivenException_ThenBadRequestResult()
        {
            // Arrange
            SpeakerService.Setup(x => x.Get(_id)).Throws(new Exception());

            // Act
            var result = await Controller.Get(_id);

            // Assert
            Assert.IsAssignableFrom<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public async Task GivenException_ThenItLogsError()
        {
            // Arrange
            var ex = new Exception();
            SpeakerService.Setup(x => x.Get(_id)).Throws(ex);

            // Act
            await Controller.Get(_id);

            // Assert
            Logger.Verify(x => x.LogError(ex, ex.Message), Times.Once());
        }
    }
}
