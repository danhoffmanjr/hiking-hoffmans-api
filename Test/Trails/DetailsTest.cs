using System;
using System.Linq;
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
        public async Task ShouldReturnTrailWithInputIdFromDbAsync()
        {
            //Arrange
            var id = Guid.Parse("08489d16-d7b1-4a90-8bef-0b4c94b50fe0");
            trailServiceMock.Setup(x => x.FindByIdAsync(id)).ReturnsAsync(TrailsMockData.TrailByIdMock);

            var validRequest = new Details.Query() { Id = id.ToString() };
            var invalidRequest = new Details.Query() { Id = It.IsAny<string>() };
            var cancellationToken = new CancellationToken();

            //Act
            Trail trail = await detailsHandler.Handle(validRequest, cancellationToken);

            //Assert
            trailServiceMock.Verify(x => x.FindByIdAsync(id), Times.Once);
            Assert.IsType<Trail>(trail);
            await Assert.ThrowsAsync<ArgumentException>(async () => await detailsHandler.Handle(invalidRequest, cancellationToken));
        }
    }
}