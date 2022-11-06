using System.Threading.Tasks;
using Moq;
using SpeakerMeet.Core.Exceptions;
using SpeakerMeet.Core.Models.DTOs;
using SpeakerMeet.Core.Models.Entities;
using SpeakerMeet.Core.Specifications;
using Xunit;

namespace SpeakerMeet.Core.Tests.Services.ConferenceServiceTests
{
    public class GetBySlug : ConferenceServiceTestBase
    {
        private readonly string _slug;

        public GetBySlug()
        {
            _slug = "test-slug";
            var conference = new Conference
            {
                Slug = _slug
            };

            Repository.Setup(x => x.Get(It.IsAny<ConferenceSpecification>())).ReturnsAsync(conference);
        }

        [Fact]
        public async Task ItReturnsConference()
        {
            // Arrange
            // Act
            var conference = await Service.Get(_slug);

            // Assert
            Assert.NotNull(conference);
            Assert.IsAssignableFrom<ConferenceResult>(conference);
        }

        [Fact]
        public async Task WhenConferenceNull_ThenEntityNotFoundException()
        {
            // Arrange
            Repository.Setup(x => x.Get(It.IsAny<ConferenceSpecification>())).ReturnsAsync((Conference?)null);

            // Act
            Task Act() => Service.Get(_slug);

            // Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(Act);
        }
    }
}