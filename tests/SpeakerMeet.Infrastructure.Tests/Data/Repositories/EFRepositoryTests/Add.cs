using System.Threading.Tasks;
using SpeakerMeet.Core.Entities;
using SpeakerMeet.Infrastructure.Data;
using SpeakerMeet.Infrastructure.Data.Repositories;
using Xunit;

namespace SpeakerMeet.Infrastructure.Tests.Data.Repositories.EFRepositoryTests
{
    public class Add : EFRepositoryTestBase
    {
        [Fact]
        public async Task ItAddsSpeaker()
        {
            // Arrange
            await using var context = new SpeakerMeetContext(Options);
            var repository = new SpeakerMeetRepository(context);
            var speaker = new Speaker();

            // Act
            await repository.Add(speaker);

            // Assert
            Assert.Contains(context.Speakers, x => x == speaker);
        }

        [Fact]
        public async Task ItReturnsNewlyAddedSpeaker()
        {
            // Arrange
            await using var context = new SpeakerMeetContext(Options);
            var repository = new SpeakerMeetRepository(context);
            var speaker = new Speaker();

            // Act
            var newlyAddedSpeaker = await repository.Add(speaker);

            // Assert
            Assert.Equal(speaker, newlyAddedSpeaker);
        }
    }
}