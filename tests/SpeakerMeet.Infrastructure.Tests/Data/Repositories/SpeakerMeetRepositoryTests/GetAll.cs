using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpeakerMeet.Core.Entities;
using SpeakerMeet.Infrastructure.Data;
using SpeakerMeet.Infrastructure.Data.Repositories;
using Xunit;

namespace SpeakerMeet.Infrastructure.Tests.Data.Repositories.SpeakerMeetRepositoryTests
{
    public class GetAll : SpeakerMeetRepositoryTestBase
    {
        public GetAll()
        {
            using var context = new SpeakerMeetContext(Options);
            context.Speakers.Add(new Speaker {Id = 1});
            context.Speakers.Add(new Speaker {Id = 2});
            context.Speakers.Add(new Speaker {Id = 3});
            context.SaveChanges();
        }

        [Fact]
        public async Task ItReturnsAllThings()
        {
            // Arrange
            await using var context = new SpeakerMeetContext(Options);
            var repository = new SpeakerMeetRepository(context);

            // Act
            var speakers = (List<Speaker>) await repository.GetAll<Speaker>();

            // Assert
            Assert.IsAssignableFrom<IEnumerable<Speaker>>(speakers);
            Assert.Equal(context.Speakers.Count(), speakers.Count);
        }
    }
}