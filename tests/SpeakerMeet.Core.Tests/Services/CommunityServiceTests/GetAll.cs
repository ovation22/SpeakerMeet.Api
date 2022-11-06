using System.Threading.Tasks;
using Moq;
using SpeakerMeet.Core.Constants;
using SpeakerMeet.Core.Models.DTOs;
using SpeakerMeet.Core.Specifications;
using Xunit;

namespace SpeakerMeet.Core.Tests.Services.CommunityServiceTests
{
    public class GetAll : CommunityServiceTestBase
    {
        [Fact]
        public async Task ItReturnsCommunities()
        {
            // Arrange
            // Act
            var communities = await Service.GetAll(0, 1, nameof(Direction.Asc));

            // Assert
            Assert.NotNull(communities);
            Assert.IsAssignableFrom<CommunitiesResult>(communities);
        }

        [Fact]
        public async Task ItCallsRepository()
        {
            // Arrange
            // Act
            await Service.GetAll(0, 1, nameof(Direction.Desc));

            // Assert
            Repository.Verify(x => x.List(It.IsAny<CommunitySpecification>()), Times.Once());
        }
    }
}
