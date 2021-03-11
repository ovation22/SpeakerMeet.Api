using Moq;
using SpeakerMeet.Api.Controllers;
using SpeakerMeet.Core.Interfaces.Logging;
using SpeakerMeet.Core.Interfaces.Services;

namespace SpeakerMeet.Api.Tests.Controllers.SearchControllerTests
{
    public class SearchControllerTestBase
    {
        protected internal SearchController Controller;
        protected internal Mock<ISearchService> SearchService;
        protected internal Mock<ILoggerAdapter<SearchController>> Logger;

        public SearchControllerTestBase()
        {
            SearchService = new Mock<ISearchService>();
            Logger = new Mock<ILoggerAdapter<SearchController>>();

            Controller = new SearchController(SearchService.Object, Logger.Object);
        }
    }
}
