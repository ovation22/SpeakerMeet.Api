using System;
using SpeakerMeet.Infrastructure.Utilities;
using Xunit;

namespace SpeakerMeet.Infrastructure.Tests.Utilities.TimeManagerTests
{
    [Trait("Category", "TimeManager")]
    public class UtcNow
    {
        [Fact]
        public void ItReturnsUtcDateTime()
        {
            // Arrange
            var timeManager = new TimeManager();

            // Act 
            var result = timeManager.UtcNow();

            // Assert
            Assert.IsAssignableFrom<DateTime>(result);
            Assert.Equal(DateTimeKind.Utc, result.Kind);
        }
    }
}
