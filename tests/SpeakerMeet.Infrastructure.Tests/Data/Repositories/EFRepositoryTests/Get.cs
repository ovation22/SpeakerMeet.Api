using System;
using System.Threading.Tasks;
using SpeakerMeet.Core.Entities;
using Xunit;

namespace SpeakerMeet.Infrastructure.Tests.Data.Repositories.EFRepositoryTests
{
    public class Get : EFRepositoryTestBase
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
            var speaker = await Repository.Get<Speaker>(x => x.Id == _id);

            // Assert
            Assert.IsAssignableFrom<Speaker>(speaker);
        }
    }
}