using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SpeakerMeet.Core.Exceptions;
using Xunit;

namespace SpeakerMeet.Api.Tests.Controllers.SpeakersControllerTests
{
    public class GetBySlug : SpeakersControllerTestBase
    {
        private readonly string _slug;

        public GetBySlug()
        {
            _slug = "test-slug";
        }

        [Fact]
        public async Task ItReturnsOkObjectResult()
        {
            // Arrange
            // Act
            var result = await Controller.GetBySlug(_slug);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task ItGetsSpeaker()
        {
            // Arrange
            // Act
            await Controller.GetBySlug(_slug);

            // Assert
            SpeakerService.Verify(x => x.Get(_slug), Times.Once());
        }

        [Fact]
        public async Task GivenEntityNotFoundException_ThenBadRequestResult()
        {
            // Arrange
            SpeakerService.Setup(x => x.Get(_slug)).Throws(new EntityNotFoundException());

            // Act
            var result = await Controller.GetBySlug(_slug);

            // Assert
            Assert.IsAssignableFrom<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public async Task GivenEntityNotFoundException_ThenItLogsWarning()
        {
            // Arrange
            var ex = new EntityNotFoundException();
            SpeakerService.Setup(x => x.Get(_slug)).Throws(ex);

            // Act
            await Controller.GetBySlug(_slug);

            // Assert
            Logger.Verify(x => x.LogWarning(ex, ex.Message), Times.Once());
        }

        [Fact]
        public async Task GivenException_ThenBadRequestResult()
        {
            // Arrange
            SpeakerService.Setup(x => x.Get(_slug)).Throws(new Exception());

            // Act
            var result = await Controller.GetBySlug(_slug);

            // Assert
            Assert.IsAssignableFrom<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public async Task GivenException_ThenItLogsError()
        {
            // Arrange
            var ex = new Exception();
            SpeakerService.Setup(x => x.Get(_slug)).Throws(ex);

            // Act
            await Controller.GetBySlug(_slug);

            // Assert
            Logger.Verify(x => x.LogError(ex, ex.Message), Times.Once());
        }
    }
}
