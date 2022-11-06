using System.Threading.Tasks;
using Moq;
using SpeakerMeet.Core.Constants;
using SpeakerMeet.Core.Models.DTOs;
using SpeakerMeet.Core.Specifications;
using Xunit;

namespace SpeakerMeet.Core.Tests.Services.SpeakerServiceTests
{
    public class GetAll : SpeakerServiceTestBase
    {
        [Fact]
        public async Task ItReturnsSpeakers()
        {
            // Arrange
            // Act
            var speakers = await Service.GetAll(0, 1, nameof(Direction.Asc));

            // Assert
            Assert.NotNull(speakers);
            Assert.IsAssignableFrom<SpeakersResult>(speakers);
        }

        [Fact]
        public async Task ItCallsRepository()
        {
            // Arrange
            // Act
            await Service.GetAll(0, 1, nameof(Direction.Desc));

            // Assert
            Repository.Verify(x => x.List(It.IsAny<SpeakerSpecification>()), Times.Once());
        }
    }
}
