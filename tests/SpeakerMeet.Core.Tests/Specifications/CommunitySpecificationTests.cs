using System;
using System.Collections.Generic;
using System.Linq;
using SpeakerMeet.Core.Entities;
using SpeakerMeet.Core.Specifications;
using Xunit;

namespace SpeakerMeet.Core.Tests.Specifications
{
    public class CommunitySpecificationTests
    {
        private readonly Guid _id;
        private readonly string _slug;

        public CommunitySpecificationTests()
        {
            _slug = "slug";
            _id = new Guid("BB2316DE-9CF6-4F17-BF72-2B77C7BD1E88");
        }

        [Fact]
        public void WhenCommunityFoundWithId_ThenCommunityReturned()
        {
            // Arrange
            var spec = new CommunitySpecification(_id);

            // Act
            var result = spec.Evaluate(GetTestCollection()).Single();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(_id, result.Id);
        }

        [Fact]
        public void WhenCommunityNotFoundWithId_ThenNull()
        {
            // Arrange
            var badCommunityId = new Guid("172B6257-582D-4453-A13F-41C6CBE4CAB2");
            var spec = new CommunitySpecification(badCommunityId);

            // Act
            var result = spec.Evaluate(GetTestCollection()).SingleOrDefault();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void WhenCommunityFoundWithSlug_ThenCommunityReturned()
        {
            // Arrange
            var spec = new CommunitySpecification(_slug);

            // Act
            var result = spec.Evaluate(GetTestCollection()).Single();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(_slug, result.Slug);
        }

        private IEnumerable<Community> GetTestCollection()
        {
            return new List<Community>
            {
                new() { Id = new Guid("D7728576-CC03-40E2-A92B-2E47FC791C60") }, 
                new() { Id = new Guid("B067FA84-7940-4D6D-9170-F0EDAD986C87") }, 
                new() { Id = _id, Slug = _slug }, 
                new() { Id = new Guid("D6506015-E4E5-4057-9B42-A112C8B08C56") }
            };
        }
    }
}
