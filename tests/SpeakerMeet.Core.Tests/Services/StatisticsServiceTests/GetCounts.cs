using System.Threading.Tasks;
using Moq;
using SpeakerMeet.Core.DTOs;
using SpeakerMeet.Core.Entities;
using Xunit;

namespace SpeakerMeet.Core.Tests.Services.StatisticsServiceTests
{
    public class GetCounts : StatisticsServiceTestBase
    {
        [Fact]
        public async Task ItReturnsStatistics()
        {
            // Arrange
            // Act
            var counts = await Service.GetCounts();

            // Assert
            Assert.NotNull(counts);
            Assert.IsAssignableFrom<CountResult>(counts);
            Repository.Verify(x => x.Count<Speaker>(), Times.Once());
            Repository.Verify(x => x.Count<Community>(), Times.Once());
            Repository.Verify(x => x.Count<Conference>(), Times.Once());
        }
    }
}