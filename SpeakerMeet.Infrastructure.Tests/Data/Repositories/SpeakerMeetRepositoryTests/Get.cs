using System.Threading.Tasks;
using SpeakerMeet.Core.Entities;
using SpeakerMeet.Infrastructure.Data;
using SpeakerMeet.Infrastructure.Data.Repositories;
using Xunit;

namespace SpeakerMeet.Infrastructure.Tests.Data.Repositories.SpeakerMeetRepositoryTests
{
    public class Get : SpeakerMeetRepositoryTestBase
    {
        public Get()
        {
            using var context = new SpeakerMeetContext(Options);
            context.Speakers.Add(new Speaker {Id = 4});
            context.SaveChanges();
        }

        [Fact]
        public async Task ItReturnsAllThings()
        {
            // Arrange
            await using var context = new SpeakerMeetContext(Options);
            var repository = new SpeakerMeetRepository(context);

            // Act
            var speaker = await repository.Get<Speaker>(x => x.Id == 4);

            // Assert
            Assert.IsAssignableFrom<Speaker>(speaker);
        }
    }
}