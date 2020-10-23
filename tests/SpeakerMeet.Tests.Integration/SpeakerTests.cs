using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using SpeakerMeet.Api;
using Xunit;

namespace SpeakerMeet.Tests.Integration
{
    public class SpeakersTests :
        IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public SpeakersTests(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Get_Speaker_ReturnsOk()
        {
            // Arrange
            const string slug = "slug";

            //Act
            var response = await _client.GetAsync($"api/Speakers/{slug}");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task WhenSpeakerNotFound_ThenBadResult()
        {
            // Arrange
            const string slug = "doesn't exist";

            //Act
            var response = await _client.GetAsync($"api/Speakers/{slug}");

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}