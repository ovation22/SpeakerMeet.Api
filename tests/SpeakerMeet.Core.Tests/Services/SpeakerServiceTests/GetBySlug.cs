using System.Threading.Tasks;
using Moq;
using SpeakerMeet.Core.Exceptions;
using SpeakerMeet.Core.Models.DTOs;
using SpeakerMeet.Core.Models.Entities;
using SpeakerMeet.Core.Specifications;
using Xunit;

namespace SpeakerMeet.Core.Tests.Services.SpeakerServiceTests
{
    public class GetBySlug : SpeakerServiceTestBase
    {
        private readonly string _slug;

        public GetBySlug()
        {
            _slug = "test-slug";
            var speaker = new Speaker
            {
                Slug = _slug
            };

            Repository.Setup(x => x.Get(It.IsAny<SpeakerSpecification>())).ReturnsAsync(speaker);
        }

        [Fact]
        public async Task ItReturnsSpeaker()
        {
            // Arrange
            // Act
            var speaker = await Service.Get(_slug);

            // Assert
            Assert.NotNull(speaker);
            Assert.IsAssignableFrom<SpeakerResult>(speaker);
        }

        [Fact]
        public async Task WhenSpeakerNull_ThenEntityNotFoundException()
        {
            // Arrange
            Repository.Setup(x => x.Get(It.IsAny<SpeakerSpecification>())).ReturnsAsync((Speaker?)null);

            // Act
            Task Act() => Service.Get(_slug);

            // Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(Act);
        }
    }
}