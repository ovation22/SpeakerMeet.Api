using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using SpeakerMeet.Core.Models.DTOs;
using Xunit;

namespace SpeakerMeet.Tests.Integration.DB.Tests
{
    [Collection(ApiTestCollection.CollectionName)]
    public class Communities
    {
        private readonly HttpClient _client;

        public Communities(ApiTestContext testContext)
        {
            _client = testContext.HttpClient;
        }

        [Fact]
        public async Task ItGetsCommunities()
        {
            // Arrange
            // Act
            var response = await _client.GetAsync("/api/Communities");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var viewResult = Assert.IsType<HttpResponseMessage>(response);
            var model = Assert.IsAssignableFrom<CommunitiesResult>(JsonSerializer.Deserialize<CommunitiesResult>(await viewResult.Content.ReadAsStringAsync()));
            Assert.NotNull(model);
        }
    }
}
