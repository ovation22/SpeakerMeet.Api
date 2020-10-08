using System.Collections.Generic;
using Moq;
using SpeakerMeet.Core.Entities;
using SpeakerMeet.Core.Interfaces.Caching;
using SpeakerMeet.Core.Interfaces.Repositories;
using SpeakerMeet.Core.Services;
using SpeakerMeet.Core.Specifications;

namespace SpeakerMeet.Core.Tests.Services.SpeakerServiceTests
{
    public class SpeakerServiceTestBase
    {
        protected internal SpeakerService Service;
        protected internal Mock<ICacheManager> Cache;
        protected internal Mock<ISpeakerMeetRepository> Repository;

        public SpeakerServiceTestBase()
        {
            Cache = new Mock<ICacheManager>();
            Repository = new Mock<ISpeakerMeetRepository>();

            Repository.Setup(x => x.List(It.IsAny<SpeakerSpecification>()))
                .ReturnsAsync(new List<Speaker>());

            Service = new SpeakerService(Cache.Object, Repository.Object);
        }
    }
}
