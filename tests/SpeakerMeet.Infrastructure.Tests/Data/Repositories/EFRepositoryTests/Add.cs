using System.Threading.Tasks;
using SpeakerMeet.Core.Entities;
using Xunit;

namespace SpeakerMeet.Infrastructure.Tests.Data.Repositories.EFRepositoryTests
{
    public class Add : EFRepositoryTestBase
    {
        public Add(ContextFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task ItAddsSpeaker()
        {
            // Arrange
            var speaker = new Speaker();

            // Act
            await Repository.Add(speaker);

            // Assert
            Assert.Contains(Context.Speakers, x => x == speaker);
        }

        [Fact]
        public async Task ItReturnsNewlyAddedSpeaker()
        {
            // Arrange
            var speaker = new Speaker();

            // Act
            var newlyAddedSpeaker = await Repository.Add(speaker);

            // Assert
            Assert.Equal(speaker, newlyAddedSpeaker);
        }
    }
}