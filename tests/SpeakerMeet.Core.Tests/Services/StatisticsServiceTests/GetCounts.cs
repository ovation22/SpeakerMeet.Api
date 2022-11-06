using System;
using System.Threading.Tasks;
using Moq;
using SpeakerMeet.Core.Models.DTOs;
using SpeakerMeet.Core.Models.Entities;
using Xunit;

namespace SpeakerMeet.Core.Tests.Services.StatisticsServiceTests
{
    public class GetCounts : StatisticsServiceTestBase
    {
        public GetCounts()
        {
            Repository.Setup(x => x.Count<Speaker>()).ReturnsAsync(1);
            Repository.Setup(x => x.Count<Community>()).ReturnsAsync(1);
            Repository.Setup(x => x.Count<Conference>()).ReturnsAsync(1);
        }

        [Fact]
        public async Task ItGetsFromRepository()
        {
            // Arrange
            Cache.Setup(x => x.GetOrCreate(It.IsAny<string>(), It.IsAny<Func<Task<CountResult>>>()))
                .Callback((string key, Func<Task<CountResult>> action) => action());

            // Act
            await Service.GetCounts();

            // Assert
            Repository.Verify(x => x.Count<Speaker>(), Times.Once());
            Repository.Verify(x => x.Count<Community>(), Times.Once());
            Repository.Verify(x => x.Count<Conference>(), Times.Once());
            Cache.Verify(x => x.GetOrCreate(It.IsAny<string>(), It.IsAny<Func<Task<CountResult>>>()), Times.Once());
        }

        [Fact]
        public async Task ItGetsFromCache()
        {
            // Arrange
            // Act
            await Service.GetCounts();

            // Assert
            Repository.Verify(x => x.Count<Speaker>(), Times.Never());
            Repository.Verify(x => x.Count<Community>(), Times.Never());
            Repository.Verify(x => x.Count<Conference>(), Times.Never());
            Cache.Verify(x => x.GetOrCreate(It.IsAny<string>(), It.IsAny<Func<Task<CountResult>>>()), Times.Once());
        }
    }
}