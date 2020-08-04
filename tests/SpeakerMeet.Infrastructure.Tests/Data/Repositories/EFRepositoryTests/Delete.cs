using System;
using System.Threading.Tasks;
using SpeakerMeet.Core.Entities;
using SpeakerMeet.Infrastructure.Data;
using SpeakerMeet.Infrastructure.Data.Repositories;
using Xunit;

namespace SpeakerMeet.Infrastructure.Tests.Data.Repositories.EFRepositoryTests
{
    public class Delete : EFRepositoryTestBase
    {
        private readonly Speaker _speaker;

        public Delete()
        {
            _speaker = new Speaker{Id = new Guid("D6012CB6-6184-4AB4-BE14-B29C61F2CB32")};
            using var context = new SpeakerMeetContext(Options);
            context.Speakers.Add(_speaker);
            context.SaveChanges();
        }

        [Fact]
        public async Task ItRemovesSpeaker()
        {
            // Arrange
            await using var context = new SpeakerMeetContext(Options);
            var repository = new SpeakerMeetRepository(context);

            // Act
            await repository.Delete(_speaker);

            // Assert
            Assert.DoesNotContain(context.Speakers, x => x == _speaker);
        }
    }
}