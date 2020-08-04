using System;
using System.Threading.Tasks;
using SpeakerMeet.Core.Entities;
using SpeakerMeet.Infrastructure.Data;
using SpeakerMeet.Infrastructure.Data.Repositories;
using Xunit;

namespace SpeakerMeet.Infrastructure.Tests.Data.Repositories.EFRepositoryTests
{
    public class Update : EFRepositoryTestBase
    {
        private readonly Speaker _speaker;

        public Update()
        {
            _speaker = new Speaker{Id = new Guid("5FD2E324-A935-484E-8F9F-F52E7921EF21")};
            using var context = new SpeakerMeetContext(Options);
            context.Speakers.Add(_speaker);
            context.SaveChanges();
        }

        [Fact]
        public async Task ItUpdatesSpeaker()
        {
            // Arrange
            await using var context = new SpeakerMeetContext(Options);
            var repository = new SpeakerMeetRepository(context);

            _speaker.Name = "Updated";
            
            // Act
            await repository.Update(_speaker);

            // Assert
            Assert.Contains(context.Speakers, x => x.Name == "Updated");
        }
    }
}