using System.Collections.Generic;
using Moq;
using SpeakerMeet.Core.Interfaces.Repositories;
using SpeakerMeet.Core.Models.Entities;
using SpeakerMeet.Core.Services;
using SpeakerMeet.Core.Specifications;

namespace SpeakerMeet.Core.Tests.Services.SpeakerPresentationServiceTests
{
    public class SpeakerPresentationServiceTestBase
    {
        protected internal SpeakerPresentationService Service;
        protected internal Mock<ISpeakerMeetRepository> Repository;

        public SpeakerPresentationServiceTestBase()
        {
            Repository = new Mock<ISpeakerMeetRepository>();

            Repository.Setup(x => x.List(It.IsAny<SpeakerPresentationSpecification>()))
                .ReturnsAsync(new List<SpeakerPresentation>());

            Service = new SpeakerPresentationService(Repository.Object);
        }
    }
}
