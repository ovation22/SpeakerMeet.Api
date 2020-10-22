using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace SpeakerMeet.Tests.Integration.DB.Tests
{
    [Collection(ApiTestCollection.CollectionName)]
    public class Speakers
    {
        private readonly HttpClient _client;

        public Speakers(ApiTestContext testContext)
        {
            _client = testContext.HttpClient;
        }

        [Fact]
        public async Task Test1()
        {
            // Arrange
            // Act
            var response = await _client.GetAsync("/api/Speakers");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
