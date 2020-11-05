using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace SpeakerMeet.Api.Tests.Controllers.SpeakersControllerTests
{
    public class GetPresentations : SpeakersControllerTestBase
    {
        private readonly Guid _id;

        public GetPresentations()
        {
            _id = new Guid("B0FB9C49-7D5F-4E2E-AD02-FDA8A92F91EC");
        }

        [Fact]
        public async Task ItReturnsOkObjectResult()
        {
            // Arrange
            // Act
            var result = await Controller.GetPresentations(_id);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task ItGetsSpeakerPresentations()
        {
            // Arrange
            // Act
            await Controller.GetPresentations(_id);

            // Assert
            SpeakerPresentationService.Verify(x => x.GetAll(_id), Times.Once());
        }

        [Fact]
        public async Task GivenException_ThenBadRequestResult()
        {
            // Arrange
            SpeakerPresentationService.Setup(x => x.GetAll(_id)).Throws(new Exception());

            // Act
            var result = await Controller.GetPresentations(_id);

            // Assert
            Assert.IsAssignableFrom<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public async Task GivenException_ThenItLogsError()
        {
            // Arrange
            var ex = new Exception();
            SpeakerPresentationService.Setup(x => x.GetAll(_id)).Throws(ex);

            // Act
            await Controller.GetPresentations(_id);

            // Assert
            Logger.Verify(x => x.LogError(ex, ex.Message), Times.Once());
        }
    }
}
