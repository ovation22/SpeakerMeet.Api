using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using SpeakerMeet.Core.DTOs;
using SpeakerMeet.Core.Entities;
using Xunit;

namespace SpeakerMeet.Core.Tests.Services.CommunityServiceTests
{
    public class GetFeatured : CommunityServiceTestBase
    {
        [Fact]
        public async Task ItReturnsFeaturedCommunities()
        {
            // Arrange
            // Act
            var communities = await Service.GetFeatured();

            // Assert
            Assert.NotNull(communities);
            Assert.IsAssignableFrom<IEnumerable<CommunitiesResult>>(communities);
        }

        [Fact]
        public async Task ItCallsRepository()
        {
            // Arrange
            // Act
            await Service.GetFeatured();

            // Assert
            Repository.Verify(x => x.GetRandom<Community>(4), Times.Once());
        }
    }
}
