using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task ShouldReturnAllTrailsFromDbAsync()
        {
            //Arrange
            dbContextMock.Setup(x => x.Trails).Returns(trailsDbSetMock.Object);
            var testContext = dbContextMock.Object;

            //Act
            var trails = await testContext.Trails.ToListAsync();

            //Assert
            Assert.Equal(2, trails.Count());
            Assert.IsType<List<Trail>>(trails);
        }
    }
}