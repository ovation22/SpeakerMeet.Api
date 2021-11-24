using System;
using System.Threading.Tasks;
using SpeakerMeet.Core.Entities;
using Xunit;

namespace SpeakerMeet.Infrastructure.Tests.Data.Repositories.SpeakerMeetRepositoryTests
{
    [Collection("ContextFixture")]
    [Trait("Category", "SpeakerMeetRepository")]
    public class Delete : SpeakerMeetRepositoryTestBase
    {
        private readonly Speaker _speaker;

        public Delete(ContextFixture fixture) : base(fixture)
        {
            _speaker = new Speaker
            {
                Id = new Guid("D6012CB6-6184-4AB4-BE14-B29C61F2CB32"),
                Name = "Test Speaker",
                Slug = "test-speaker",
                Location = "Tampa, FL",
                Description = "Test Speaker from Tampa, FL"
            };
            Context.Speakers.Add(_speaker);
            Context.SaveChanges();
        }

        [Fact]
        public async Task ItRemovesSpeaker()
        {
            // Arrange
            // Act
            await Repository.Delete(_speaker);

            // Assert
            Assert.DoesNotContain(Context.Speakers, x => x == _speaker);
        }
    }
}