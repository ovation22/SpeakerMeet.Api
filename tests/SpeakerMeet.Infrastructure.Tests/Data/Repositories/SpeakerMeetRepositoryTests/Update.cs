using System;
using System.Threading.Tasks;
using SpeakerMeet.Core.Entities;
using Xunit;

namespace SpeakerMeet.Infrastructure.Tests.Data.Repositories.SpeakerMeetRepositoryTests
{
    [Collection("ContextFixture")]
    public class Update : SpeakerMeetRepositoryTestBase
    {
        private readonly Speaker _speaker;

        public Update(ContextFixture fixture) : base(fixture)
        {
            _speaker = new Speaker { Id = new Guid("5FD2E324-A935-484E-8F9F-F52E7921EF21") };
            Context.Speakers.Add(_speaker);
            Context.SaveChanges();
        }

        [Fact]
        public async Task ItUpdatesSpeaker()
        {
            // Arrange
            _speaker.Name = "Updated";

            // Act
            await Repository.Update(_speaker);

            // Assert
            Assert.Contains(Context.Speakers, x => x.Name == "Updated");
        }
    }
}