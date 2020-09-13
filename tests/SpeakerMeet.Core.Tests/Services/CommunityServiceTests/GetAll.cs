using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using SpeakerMeet.Core.DTOs;
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
            var communities = await Service.GetAll(0, 1);

            // Assert
            Assert.NotNull(communities);
            Assert.IsAssignableFrom<CommunitiesResult>(communities);
        }

        [Fact]
        public async Task ItCallsRepository()
        {
            // Arrange
            // Act
            await Service.GetAll(0, 1);

            // Assert
            Repository.Verify(x => x.List(It.IsAny<CommunitySpecification>()), Times.Once());
        }
    }
}
