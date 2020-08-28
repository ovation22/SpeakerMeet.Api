using System.Threading.Tasks;
using Moq;
using SpeakerMeet.Core.DTOs;
using SpeakerMeet.Core.Entities;
using SpeakerMeet.Core.Specifications;
using Xunit;

namespace SpeakerMeet.Core.Tests.Services.ConferenceServiceTests
{
    public class GetBySlug : ConferenceServiceTestBase
    {
        private readonly string _slug;
        private readonly Conference _conference;

        public GetBySlug()
        {
            _slug = "test-slug";
            _conference = new Conference
                {
                    Slug = _slug
                };

            Repository.Setup(x => x.Get(It.IsAny<ConferenceSpecification>())).ReturnsAsync(_conference);
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
    }
}