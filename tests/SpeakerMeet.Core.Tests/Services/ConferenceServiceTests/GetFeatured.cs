using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using SpeakerMeet.Core.DTOs;
using SpeakerMeet.Core.Entities;
using SpeakerMeet.Core.Specifications;
using Xunit;

namespace SpeakerMeet.Core.Tests.Services.ConferenceServiceTests
{
    public class GetFeatured : ConferenceServiceTestBase
    {
        public GetFeatured()
        {
            Repository.Setup(x => x.List(It.IsAny<ConferenceRandomSpecification>()))
                .ReturnsAsync(new List<Conference>());
        }

        [Fact]
        public async Task ItReturnsFeaturedConferences()
        {
            // Arrange
            // Act
            var conferences = await Service.GetFeatured();

            // Assert
            Assert.NotNull(conferences);
            Assert.IsAssignableFrom<IEnumerable<ConferenceFeatured>>(conferences);
        }

        [Fact]
        public async Task ItCallsRepository()
        {
            // Arrange
            // Act
            await Service.GetFeatured();

            // Assert
            Repository.Verify(x => x.List(It.IsAny<ConferenceRandomSpecification>()), Times.Once());
        }
    }
}
