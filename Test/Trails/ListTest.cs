using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Trails;
using Domain.Entities;
using Moq;
using Xunit;

namespace Test.Trails
{
    public class ListTest
    {
        private readonly Mock<ITrailsService> trailServiceMock;
        private readonly List.Handler listHandler;

        public ListTest()
        {
            trailServiceMock = new Mock<ITrailsService>();
            listHandler = new List.Handler(trailServiceMock.Object);
        }

        [Fact]
        public async Task Handle_ListAsync_ReturnsAllTrails()
        {
            //Arrange
            trailServiceMock.Setup(x => x.ListAsync()).ReturnsAsync(TrailsMockData.TrailListMock);

            var request = new List.Query();
            var cancellationToken = new CancellationToken();

            //Act
            List<Trail> trails = await listHandler.Handle(request, cancellationToken);

            //Assert
            trailServiceMock.Verify(x => x.ListAsync(), Times.Once);
            Assert.IsType<List<Trail>>(trails);
        }
    }
}