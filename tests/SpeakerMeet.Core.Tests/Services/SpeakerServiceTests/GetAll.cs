using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using SpeakerMeet.Core.DTOs;
using SpeakerMeet.Core.Entities;
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
            var speakers = await Service.GetAll();

            // Assert
            Assert.NotNull(speakers);
            Assert.IsAssignableFrom<IEnumerable<SpeakersResult>>(speakers);
        }

        [Fact]
        public async Task ItCallsRepository()
        {
            // Arrange
            // Act
            await Service.GetAll();

            // Assert
            Repository.Verify(x => x.GetAll<Speaker>(), Times.Once());
        }
    }
}
