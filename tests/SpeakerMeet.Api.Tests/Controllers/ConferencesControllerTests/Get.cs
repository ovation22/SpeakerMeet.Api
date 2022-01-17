using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SpeakerMeet.Core.Exceptions;
using Xunit;

namespace SpeakerMeet.Api.Tests.Controllers.ConferencesControllerTests
{
    public class Get : ConferencesControllerTestBase
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
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task ItGetsConference()
        {
            // Arrange
            // Act
            await Controller.Get(_id);

            // Assert
            ConferenceService.Verify(x => x.Get(_id), Times.Once());
        }

        [Fact]
        public async Task GivenEntityNotFoundException_ThenNotFoundResult()
        {
            // Arrange
            ConferenceService.Setup(x => x.Get(_id)).Throws(new EntityNotFoundException());

            // Act
            var result = await Controller.Get(_id);

            // Assert
            Assert.IsAssignableFrom<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public async Task GivenEntityNotFoundException_ThenItLogsWarning()
        {
            // Arrange
            var ex = new EntityNotFoundException();
            ConferenceService.Setup(x => x.Get(_id)).Throws(ex);

            // Act
            await Controller.Get(_id);

            // Assert
            Logger.Verify(x => x.LogWarning(ex, ex.Message), Times.Once());
        }

        [Fact]
        public async Task GivenException_ThenBadRequestResult()
        {
            // Arrange
            ConferenceService.Setup(x => x.Get(_id)).Throws(new Exception());

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
            ConferenceService.Setup(x => x.Get(_id)).Throws(ex);

            // Act
            await Controller.Get(_id);

            // Assert
            Logger.Verify(x => x.LogError(ex, ex.Message), Times.Once());
        }
    }
}
