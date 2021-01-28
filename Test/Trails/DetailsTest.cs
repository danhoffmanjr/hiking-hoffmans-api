using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Trails;
using Domain.Entities;
using Moq;
using Xunit;

namespace Test.Trails
{
    public class DetailsTest
    {
        private readonly Mock<ITrailsService> trailServiceMock;
        private readonly Details.Handler detailsHandler;

        public DetailsTest()
        {
            trailServiceMock = new Mock<ITrailsService>();
            detailsHandler = new Details.Handler(trailServiceMock.Object);
        }

        [Fact]
        public async Task Handle_CallsFindByIdAsyncWithValidGuid_ReturnsTrailForGivenId()
        {
            //Arrange
            var id = Guid.Parse("08489d16-d7b1-4a90-8bef-0b4c94b50fe0");
            trailServiceMock.Setup(x => x.FindByIdAsync(id)).ReturnsAsync(TrailsMockData.TrailByIdMock);

            var validGuidRequest = new Details.Query() { Id = id };
            var cancellationToken = new CancellationToken();

            //Act
            Trail trail = await detailsHandler.Handle(validGuidRequest, cancellationToken);

            //Assert
            trailServiceMock.Verify(x => x.FindByIdAsync(id), Times.Once);
            Assert.IsType<Trail>(trail);
            Assert.Equal("Trail By Id", trail.Name);
        }

        [Fact]
        public async Task Handle_InvalidGuidIsProvided_ThrowsArgumentException()
        {
            //Arrange
            var invalidGuidRequest = new Details.Query() { Id = It.IsAny<Guid>() };
            var cancellationToken = new CancellationToken();

            //Assert with Action
            await Assert.ThrowsAsync<ArgumentException>(async () => await detailsHandler.Handle(invalidGuidRequest, cancellationToken));
        }
    }
}