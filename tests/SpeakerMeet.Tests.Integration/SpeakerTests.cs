using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace SpeakerMeet.Tests.Integration;

[Collection("Sequential")]
public class SpeakersTests :
    IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly CustomWebApplicationFactory<Program> _factory;

    public SpeakersTests(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task Get_Speaker_ReturnsOk()
    {
        // Arrange
        var client = _factory.CreateClient();
        const string slug = "test-speaker";

        //Act
        using HttpResponseMessage response = await client.GetAsync($"api/Speakers/{slug}");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task WhenSpeakerNotFound_ThenNotFound()
    {
        // Arrange
        var client = _factory.CreateClient();
        const string slug = "doesn't exist";

        //Act
        HttpResponseMessage response = await client.GetAsync($"api/Speakers/{slug}");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}