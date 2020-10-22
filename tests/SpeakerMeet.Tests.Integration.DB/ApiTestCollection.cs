using Xunit;

namespace SpeakerMeet.Tests.Integration.DB
{
    [CollectionDefinition(CollectionName)]
    public class ApiTestCollection : ICollectionFixture<ApiTestContext>
    {
        public const string CollectionName = "API Test";
    }
}
