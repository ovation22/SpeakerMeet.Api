using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using SpeakerMeet.Core.DTOs;
using SpeakerMeet.Core.Entities;
using SpeakerMeet.Core.Specifications;
using Xunit;

namespace SpeakerMeet.Core.Tests.Services.SpeakerServiceTests
{
    public class GetFeatured : SpeakerServiceTestBase
    {
        public GetFeatured()
        {
            Repository.Setup(x => x.List(It.IsAny<SpeakerRandomSpecification>()))
                .ReturnsAsync(new List<Speaker>());
        }

        [Fact]
        public async Task ItReturnsConferences()
        {
            // Arrange
            // Act
            var speakers = await Service.GetFeatured();

            // Assert
            Assert.NotNull(speakers);
            Assert.IsAssignableFrom<IEnumerable<SpeakerFeatured>>(speakers);
        }

        [Fact]
        public async Task ItGetsFromRepository()
        {
            // Arrange
            Cache.Setup(x => x.GetOrCreate(It.IsAny<string>(), It.IsAny<Func<Task<IEnumerable<SpeakerFeatured>>>>()))
                .Callback((string key, Func<Task<IEnumerable<SpeakerFeatured>>> action) => action());

            // Act
            await Service.GetFeatured();

            // Assert
            Repository.Verify(x => x.List(It.IsAny<SpeakerRandomSpecification>()), Times.Once());
        }

        [Fact]
        public async Task ItGetsFromCache()
        {
            // Arrange
            // Act
            await Service.GetFeatured();

            // Assert
            Repository.Verify(x => x.List(It.IsAny<SpeakerRandomSpecification>()), Times.Never());
            Cache.Verify(x => x.GetOrCreate(It.IsAny<string>(), It.IsAny<Func<Task<IEnumerable<SpeakerFeatured>>>>()), Times.Once());
        }
    }
}
