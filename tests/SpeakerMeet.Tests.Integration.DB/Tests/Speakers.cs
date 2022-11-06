using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using SpeakerMeet.Core.Models.DTOs;
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
        public async Task ItGetsSpeakers()
        {
            // Arrange
            // Act
            var response = await _client.GetAsync("/api/Speakers");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var viewResult = Assert.IsType<HttpResponseMessage>(response);
            var model = Assert.IsAssignableFrom<SpeakersResult>(JsonSerializer.Deserialize<SpeakersResult>(await viewResult.Content.ReadAsStringAsync()));
            Assert.NotNull(model);
        }
    }
}
