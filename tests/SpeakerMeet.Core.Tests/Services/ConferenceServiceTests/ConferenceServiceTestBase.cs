using System.Collections.Generic;
using Moq;
using SpeakerMeet.Core.Entities;
using SpeakerMeet.Core.Interfaces.Caching;
using SpeakerMeet.Core.Interfaces.Repositories;
using SpeakerMeet.Core.Services;
using SpeakerMeet.Core.Specifications;

namespace SpeakerMeet.Core.Tests.Services.ConferenceServiceTests
{
    public class ConferenceServiceTestBase
    {
        protected internal ConferenceService Service;
        protected internal Mock<ICacheManager> Cache;
        protected internal Mock<ISpeakerMeetRepository> Repository;

        public ConferenceServiceTestBase()
        {
            Cache = new Mock<ICacheManager>();
            Repository = new Mock<ISpeakerMeetRepository>();

            Repository.Setup(x => x.List(It.IsAny<ConferenceSpecification>()))
                .ReturnsAsync(new List<Conference>());

            Service = new ConferenceService(Cache.Object, Repository.Object);
        }
    }
}
