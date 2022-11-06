using System.Collections.Generic;
using Moq;
using SpeakerMeet.Core.Interfaces.Caching;
using SpeakerMeet.Core.Interfaces.Repositories;
using SpeakerMeet.Core.Models.Entities;
using SpeakerMeet.Core.Services;
using SpeakerMeet.Core.Specifications;

namespace SpeakerMeet.Core.Tests.Services.CommunityServiceTests
{
    public class CommunityServiceTestBase
    {
        protected internal CommunityService Service;
        protected internal Mock<ICacheManager> Cache;
        protected internal Mock<ISpeakerMeetRepository> Repository;

        public CommunityServiceTestBase()
        {
            Cache = new Mock<ICacheManager>();
            Repository = new Mock<ISpeakerMeetRepository>();

            Repository.Setup(x => x.List(It.IsAny<CommunitySpecification>()))
                .ReturnsAsync(new List<Community>());

            Service = new CommunityService(Cache.Object, Repository.Object);
        }
    }
}
