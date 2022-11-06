using System.Threading.Tasks;
using Moq;
using SpeakerMeet.Core.Constants;
using SpeakerMeet.Core.Models.DTOs;
using SpeakerMeet.Core.Specifications;
using Xunit;

namespace SpeakerMeet.Core.Tests.Services.ConferenceServiceTests
{
    public class GetAll : ConferenceServiceTestBase
    {
        [Fact]
        public async Task ItReturnsConferences()
        {
            // Arrange
            // Act
            var conferences = await Service.GetAll(0, 1, nameof(Direction.Asc));

            // Assert
            Assert.NotNull(conferences);
            Assert.IsAssignableFrom<ConferencesResult>(conferences);
        }

        [Fact]
        public async Task ItCallsRepository()
        {
            // Arrange
            // Act
            await Service.GetAll(0, 1, nameof(Direction.Desc));

            // Assert
            Repository.Verify(x => x.List(It.IsAny<ConferenceSpecification>()), Times.Once());
        }
    }
}
