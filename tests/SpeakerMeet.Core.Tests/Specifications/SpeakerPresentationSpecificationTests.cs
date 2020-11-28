using System;
using System.Collections.Generic;
using System.Linq;
using SpeakerMeet.Core.Entities;
using SpeakerMeet.Core.Specifications;
using Xunit;

namespace SpeakerMeet.Core.Tests.Specifications
{
    public class SpeakerPresentationSpecificationTests
    {
        private readonly Guid _speakerId;

        public SpeakerPresentationSpecificationTests()
        {
            _speakerId = new Guid("5ECBA9BC-E1D3-4CE9-813E-EC91F48D3927");
        }

        [Fact]
        public void WhenSpeakerPresentationFoundWithSpeakerId_ThenSpeakerPresentationReturned()
        {
            // Arrange
            var spec = new SpeakerPresentationSpecification(_speakerId);

            // Act
            var result = GetTestCollection()
                .AsQueryable()
                .Where(spec.WhereExpressions.Single());

            // Assert
            Assert.NotNull(result);
            Assert.Contains(result, x => x.SpeakerId == _speakerId);
        }

        [Fact]
        public void WhenSpeakerNotFoundWithId_ThenEmpty()
        {
            // Arrange
            var badSpeakerId = new Guid("A9D08DC2-C529-480A-ABD6-08D9BC3574C8");
            var spec = new SpeakerPresentationSpecification(badSpeakerId);

            // Act
            var result = GetTestCollection()
                .AsQueryable()
                .Where(spec.WhereExpressions.Single());

            // Assert
            Assert.Empty(result);
        }

        private IEnumerable<SpeakerPresentation> GetTestCollection()
        {
            return new List<SpeakerPresentation>
            {
                new() { SpeakerId = new Guid("00AB8E90-F7EF-4BF2-8277-F1733EB323E5") }, 
                new() { SpeakerId = new Guid("3D031965-BA62-46F1-AA87-6ED204B4A5CF") }, 
                new() { SpeakerId = _speakerId }, 
                new() { SpeakerId = new Guid("45DB5C19-0CF1-4625-8829-67E157AF5DED") }
            };
        }
    }
}
