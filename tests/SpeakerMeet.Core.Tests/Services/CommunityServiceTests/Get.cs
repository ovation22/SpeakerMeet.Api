using System;
using System.Threading.Tasks;
using Moq;
using SpeakerMeet.Core.DTOs;
using SpeakerMeet.Core.Entities;
using SpeakerMeet.Core.Exceptions;
using SpeakerMeet.Core.Specifications;
using Xunit;

namespace SpeakerMeet.Core.Tests.Services.CommunityServiceTests
{
    public class Get : CommunityServiceTestBase
    {
        private readonly Guid _id;

        public Get()
        {
            _id = new Guid("B703FC6F-B72D-4048-9FEA-5264A50F8363");
            var community = new Community()
            {
                Id = _id
            };

            Repository.Setup(x => x.Get(It.IsAny<CommunitySpecification>())).ReturnsAsync(community);
        }

        [Fact]
        public async Task ItReturnsCommunity()
        {
            // Arrange
            // Act
            var community = await Service.Get(_id);

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
            Task Act() => Service.Get(_id);

            // Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(Act);
        }
    }
}