using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using SpeakerMeet.Core.DTOs;
using SpeakerMeet.Core.Entities;
using SpeakerMeet.Core.Specifications;
using Xunit;

namespace SpeakerMeet.Core.Tests.Services.CommunityServiceTests
{
    public class GetFeatured : CommunityServiceTestBase
    {
        public GetFeatured()
        {
            Repository.Setup(x => x.List(It.IsAny<CommunityRandomSpecification>()))
                .ReturnsAsync(new List<Community>());
        }

        [Fact]
        public async Task ItReturnsFeaturedCommunities()
        {
            // Arrange
            // Act
            var communities = await Service.GetFeatured();

            // Assert
            Assert.NotNull(communities);
            Assert.IsAssignableFrom<IEnumerable<CommunityFeatured>>(communities);
        }

        [Fact]
        public async Task ItGetsFromRepository()
        {
            // Arrange
            Cache.Setup(x => x.GetOrCreate(It.IsAny<string>(), It.IsAny<Func<Task<IEnumerable<CommunityFeatured>>>>()))
                .Callback((string _, Func<Task<IEnumerable<CommunityFeatured>>> action) => action());

            // Act
            await Service.GetFeatured();

            // Assert
            Repository.Verify(x => x.List(It.IsAny<CommunityRandomSpecification>()), Times.Once());
        }

        [Fact]
        public async Task ItGetsFromCache()
        {
            // Arrange
            // Act
            await Service.GetFeatured();

            // Assert
            Repository.Verify(x => x.List(It.IsAny<CommunityRandomSpecification>()), Times.Never());
            Cache.Verify(x => x.GetOrCreate(It.IsAny<string>(), It.IsAny<Func<Task<IEnumerable<CommunityFeatured>>>>()), Times.Once());
        }
    }
}
