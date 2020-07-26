using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using SpeakerMeet.Core.DTOs;
using SpeakerMeet.Core.Entities;
using Xunit;

namespace SpeakerMeet.Core.Tests.Services.ConferenceServiceTests
{
    public class GetFeatured : ConferenceServiceTestBase
    {
        [Fact]
        public async Task ItReturnsFeaturedConferences()
        {
            // Arrange
            // Act
            var conferences = await Service.GetFeatured();

            // Assert
            Assert.NotNull(conferences);
            Assert.IsAssignableFrom<IEnumerable<ConferencesResult>>(conferences);
        }

        [Fact]
        public async Task ItCallsRepository()
        {
            // Arrange
            // Act
            await Service.GetFeatured();

            // Assert
            Repository.Verify(x => x.GetRandom<Conference>(4), Times.Once());
        }
    }
}
