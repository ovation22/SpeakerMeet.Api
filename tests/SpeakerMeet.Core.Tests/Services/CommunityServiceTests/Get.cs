using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Moq;
using SpeakerMeet.Core.DTOs;
using SpeakerMeet.Core.Entities;
using Xunit;

namespace SpeakerMeet.Core.Tests.Services.CommunityServiceTests
{
    public class Get : CommunityServiceTestBase
    {
        private readonly Guid _id;
        private readonly Community _community;

        public Get()
        {
            _id = new Guid("B703FC6F-B72D-4048-9FEA-5264A50F8363");
            _community = new Community
                {
                    Id = _id
                };

            Repository.Setup(x => x.Get(It.IsAny<Expression<Func<Community, bool>>>())).ReturnsAsync(_community);
        }

        [Fact]
        public async Task ItReturnsCommunity()
        {
            // Arrange
            // Act
            var community = await Service.Get(_id);

            // Assert
            Assert.NotNull(community);
            Assert.IsAssignableFrom<CommunityResult>(community);
        }
    }
}