using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using SpeakerMeet.Core.Models.DTOs;
using Xunit;

namespace SpeakerMeet.Tests.Integration.DB.Tests
{
    [Collection(ApiTestCollection.CollectionName)]
    public class Conferences
    {
        private readonly HttpClient _client;

        public Conferences(ApiTestContext testContext)
        {
            _client = testContext.HttpClient;
        }

        [Fact]
        public async Task ItGetsConferences()
        {
            // Arrange
            // Act
            var response = await _client.GetAsync("/api/Conferences");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var viewResult = Assert.IsType<HttpResponseMessage>(response);
            var model = Assert.IsAssignableFrom<ConferencesResult>(JsonSerializer.Deserialize<ConferencesResult>(await viewResult.Content.ReadAsStringAsync()));
            Assert.NotNull(model);
        }
    }
}
