using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using SpeakerMeet.Core.DTOs;
using SpeakerMeet.Core.Entities;
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
            var conferences = await Service.GetAll();

            // Assert
            Assert.NotNull(conferences);
            Assert.IsAssignableFrom<IEnumerable<ConferencesResult>>(conferences);
        }

        [Fact]
        public async Task ItCallsRepository()
        {
            // Arrange
            // Act
            await Service.GetAll();

            // Assert
            Repository.Verify(x => x.GetAll<Conference>(), Times.Once());
        }
    }
}
