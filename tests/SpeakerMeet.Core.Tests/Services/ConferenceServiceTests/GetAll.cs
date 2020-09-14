using System.Threading.Tasks;
using Moq;
using SpeakerMeet.Core.DTOs;
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
            var conferences = await Service.GetAll(0, 1);

            // Assert
            Assert.NotNull(conferences);
            Assert.IsAssignableFrom<ConferencesResult>(conferences);
        }

        [Fact]
        public async Task ItCallsRepository()
        {
            // Arrange
            // Act
            await Service.GetAll(0, 1);

            // Assert
            Repository.Verify(x => x.List(It.IsAny<ConferenceSpecification>()), Times.Once());
        }
    }
}
