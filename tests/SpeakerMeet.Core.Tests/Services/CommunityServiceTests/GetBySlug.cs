using System.Threading.Tasks;
using Moq;
using SpeakerMeet.Core.DTOs;
using SpeakerMeet.Core.Entities;
using SpeakerMeet.Core.Exceptions;
using SpeakerMeet.Core.Specifications;
using Xunit;

namespace SpeakerMeet.Core.Tests.Services.CommunityServiceTests
{
    public class GetBySlug : CommunityServiceTestBase
    {
        private readonly string _slug;

        public GetBySlug()
        {
            _slug = "test-slug";
            var community = new Community()
            {
                Slug = _slug
            };

            Repository.Setup(x => x.Get(It.IsAny<CommunitySpecification>())).ReturnsAsync(community);
        }

        [Fact]
        public async Task ItReturnsCommunity()
        {
            // Arrange
            // Act
            var community = await Service.Get(_slug);

            // Assert
            Assert.NotNull(community);
            Assert.IsAssignableFrom<CommunityResult>(community);
        }

        [Fact]
        public async Task WhenCommunityNull_ThenEntityNotFoundException()
        {
            // Arrange
            Repository.Setup(x => x.Get(It.IsAny<CommunitySpecification>())).ReturnsAsync((Community?)null);

            // Act
            Task Act() => Service.Get(_slug);

            // Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(Act);
        }
    }
}