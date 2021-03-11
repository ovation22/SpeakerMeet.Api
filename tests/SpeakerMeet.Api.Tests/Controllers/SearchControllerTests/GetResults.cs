using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace SpeakerMeet.Api.Tests.Controllers.SearchControllerTests
{
    public class GetResults : SearchControllerTestBase
    {
        private readonly string _term;

        public GetResults()
        {
            _term = "jon calloway";
        }

        [Fact]
        public async Task ItReturnsOkObjectResult()
        {
            // Arrange
            // Act
            var result = await Controller.GetResults(_term);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task ItGetsSpeaker()
        {
            // Arrange
            // Act
            await Controller.GetResults(_term);

            // Assert
            SearchService.Verify(x => x.Search(_term), Times.Once());
        }

        [Fact]
        public async Task GivenException_ThenBadRequestResult()
        {
            // Arrange
            SearchService.Setup(x => x.Search(_term)).Throws(new Exception());

            // Act
            var result = await Controller.GetResults(_term);

            // Assert
            Assert.IsAssignableFrom<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public async Task GivenException_ThenItLogsError()
        {
            // Arrange
            var ex = new Exception();
            SearchService.Setup(x => x.Search(_term)).Throws(ex);

            // Act
            await Controller.GetResults(_term);

            // Assert
            Logger.Verify(x => x.LogError(ex, ex.Message), Times.Once());
        }
    }
}
