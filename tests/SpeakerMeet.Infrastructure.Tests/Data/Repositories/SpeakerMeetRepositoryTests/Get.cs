using System;
using System.Threading.Tasks;
using SpeakerMeet.Core.Entities;
using SpeakerMeet.Core.Specifications;
using Xunit;

namespace SpeakerMeet.Infrastructure.Tests.Data.Repositories.SpeakerMeetRepositoryTests
{
    [Collection("ContextFixture")]
    [Trait("Category", "SpeakerMeetRepository")]
    public class Get : SpeakerMeetRepositoryTestBase
    {
        private readonly Guid _id;

        public Get(ContextFixture fixture) : base(fixture)
        {
            _id = Guid.NewGuid();

            Context.Speakers.Add(new Speaker { Id = _id });
            Context.SaveChanges();
        }

        [Fact]
        public async Task ItReturnsSpeaker()
        {
            // Arrange
            // Act
            var speaker = await Repository.Get(new SpeakerSpecification(_id));

            // Assert
            Assert.IsAssignableFrom<Speaker>(speaker);
        }
    }
}