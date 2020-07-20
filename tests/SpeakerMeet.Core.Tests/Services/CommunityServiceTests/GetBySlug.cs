using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Moq;
using SpeakerMeet.Core.DTOs;
using SpeakerMeet.Core.Entities;
using Xunit;

namespace SpeakerMeet.Core.Tests.Services.CommunityServiceTests
{
    public class GetBySlug : CommunityServiceTestBase
    {
        private readonly string _slug;
        private readonly Community _community;

        public GetBySlug()
        {
            _slug = "test-slug";
            _community = new Community
                {
                    Slug = _slug
                };

            Repository.Setup(x => x.Get(It.IsAny<Expression<Func<Community, bool>>>())).ReturnsAsync(_community);
        }

        [Fact]
        public async Task ItReturnsCommunity()
        {
            // Arrange
            // Act
            var community = await Service.Get(_slug);

            // Assert
            Assert.NotNull(community);
            Assert.IsAssignableFrom<CommunityResult>(community);
        }
    }
}