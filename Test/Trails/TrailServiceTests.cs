using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Application.Errors;
using Application.Interfaces;
using Application.Trails;
using Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using Xunit;

namespace Test.Trails
{
    public class TrailServiceTests
    {
        private Mock<hhDbContext> dbContextMock;
        private Mock<DbSet<Trail>> trailsDbSetMock;
        public TrailServiceTests()
        {
            dbContextMock = new Mock<hhDbContext>();
            trailsDbSetMock = TrailsMockData.TrailListMockAsQueryable.BuildMockDbSet();
        }

        [Fact]
        public async Task ListAsync_TrailsExistInDb_ReturnsAllTrails()
        {
            //Arrange
            dbContextMock.Setup(x => x.Trails).Returns(trailsDbSetMock.Object);
            var testService = new TrailsService(dbContextMock.Object);

            //Act
            var trails = await testService.ListAsync();

            //Assert
            Assert.Equal(3, trails.Count());
            Assert.IsType<List<Trail>>(trails);
        }

        [Fact]
        public async Task FindByIdAsync_TrailForGivenIdExists_ReturnsTrailForGivenId()
        {
            //Arrange
            var id = Guid.Parse("08489d16-d7b1-4a90-8bef-0b4c94b50fe0");

            dbContextMock.Setup(x => x.Trails).Returns(trailsDbSetMock.Object);
            var testService = new TrailsService(dbContextMock.Object);

            //Act
            var trail = await testService.FindByIdAsync(id);

            //Assert
            Assert.IsType<Trail>(trail);
            Assert.Equal("Trail By Id", trail.Name);
        }

        [Fact]
        public async Task FindByIdAsync_TrailForGivenIdDoesNotExist_ThrowsRestException()
        {
            //Arrange
            var notFoundId = Guid.Parse("08489d16-d7b1-AAAA-8888-0b4c94b50fe2");

            dbContextMock.Setup(x => x.Trails).Returns(trailsDbSetMock.Object);
            var testService = new TrailsService(dbContextMock.Object);

            //Assert with Action
            await Assert.ThrowsAsync<RestException>(() => testService.FindByIdAsync(notFoundId));
        }
    }
}