using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using SpeakerMeet.Core.DTOs;
using SpeakerMeet.Core.Entities;
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
            var speakers = await Service.GetAll();

            // Assert
            Assert.NotNull(speakers);
            Assert.IsAssignableFrom<IEnumerable<CommunitiesResult>>(speakers);
        }

        [Fact]
        public async Task ItCallsRepository()
        {
            // Arrange
            // Act
            await Service.GetAll();

            // Assert
            Repository.Verify(x => x.GetAll<Community>(), Times.Once());
        }
    }
}
