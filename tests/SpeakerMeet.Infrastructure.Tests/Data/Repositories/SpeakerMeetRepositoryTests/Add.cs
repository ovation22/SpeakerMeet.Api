using System.Threading.Tasks;
using SpeakerMeet.Core.Entities;
using Xunit;

namespace SpeakerMeet.Infrastructure.Tests.Data.Repositories.SpeakerMeetRepositoryTests
{
    [Collection("ContextFixture")]
    [Trait("Category", "SpeakerMeetRepository")]
    public class Add : SpeakerMeetRepositoryTestBase
    {
        public Add(ContextFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task ItAddsSpeaker()
        {
            // Arrange
            var speaker = new Speaker
            {
                Name = "Test Speaker",
                Slug = "test-speaker",
                Location = "Tampa, FL",
                Description = "Test Speaker from Tampa, FL"
            };

            // Act
            await Repository.Add(speaker);

            // Assert
            Assert.Contains(Context.Speakers, x => x == speaker);
        }

        [Fact]
        public async Task ItReturnsNewlyAddedSpeaker()
        {
            // Arrange
            var speaker = new Speaker
            {
                Name = "Test Speaker",
                Slug = "test-speaker",
                Location = "Tampa, FL",
                Description = "Test Speaker from Tampa, FL"
            };

            // Act
            var newlyAddedSpeaker = await Repository.Add(speaker);

            // Assert
            Assert.Equal(speaker, newlyAddedSpeaker);
        }
    }
}