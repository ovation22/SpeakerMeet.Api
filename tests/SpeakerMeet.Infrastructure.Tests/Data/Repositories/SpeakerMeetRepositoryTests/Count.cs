using System;
using System.Linq;
using System.Threading.Tasks;
using SpeakerMeet.Core.Entities;
using SpeakerMeet.Infrastructure.Data;
using SpeakerMeet.Infrastructure.Data.Repositories;
using Xunit;

namespace SpeakerMeet.Infrastructure.Tests.Data.Repositories.SpeakerMeetRepositoryTests
{
    public class Count : SpeakerMeetRepositoryTestBase
    {
        public Count()
        {
            using var context = new SpeakerMeetContext(Options);
            context.Speakers.Add(new Speaker {Id = Guid.NewGuid()});
            context.SaveChanges();
        }

        [Fact]
        public async Task ItReturnsSpeaker()
        {
            // Arrange
            await using var context = new SpeakerMeetContext(Options);
            var repository = new SpeakerMeetRepository(context);

            // Act
            var count = await repository.Count<Speaker>();

            // Assert
            Assert.IsAssignableFrom<int>(count);
            Assert.Equal(context.Speakers.Count(), count);
        }
    }
}