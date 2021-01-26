using System.Threading.Tasks;
using Application.Interfaces;
using Application.Trails;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Moq;
using Test.Shared.Identity;
using Xunit;

namespace Test.Trails
{
    public class CreateTest
    {
        private Mock<ITrailsService> trailsServiceMock;
        private Mock<UserManager<User>> userManager;
        private IUserAccessor userAccessor;
        private readonly Create.Handler createHandler;

        public CreateTest()
        {
            trailsServiceMock = new Mock<ITrailsService>();
            userManager = MockHelpers.MockUserManager<User>();
            userAccessor = MockHelpers.TestUserAccessor();

            trailsServiceMock.Setup(x => x.AddAsync(It.IsAny<Trail>())).ReturnsAsync(true);
            trailsServiceMock.Setup(t => t.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(TrailsMockData.CreateTrail_US_Mock);
            userManager.Setup(s => s.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(MockHelpers.MockProUser);
            createHandler = new Create.Handler(userManager.Object, userAccessor, trailsServiceMock.Object);
        }

        [Fact]
        public async Task Handle_CallsAddAsyncWithValidRequest_SavesAndReturnsNewTrail()
        {
            //Arrange
            var validRequest = TrailsMockData.Trail_CreateQuery_US_Mock;

            //Act
            Trail newTrail = await createHandler.Handle(validRequest, MockHelpers.CancellationToken);

            //Assert
            trailsServiceMock.Verify(x => x.AddAsync(It.IsAny<Trail>()), Times.Once);
            trailsServiceMock.Verify(x => x.FindByNameAsync(It.IsAny<string>()), Times.Once);
            userManager.Verify(x => x.FindByIdAsync(It.IsAny<string>()), Times.Once);
            Assert.IsType<Trail>(newTrail);
            Assert.Equal("Created Trail", newTrail.Name);
        }
    }
}