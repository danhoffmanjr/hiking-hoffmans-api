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
        public async Task ShouldReturnAllTrailsFromDbAsync()
        {
            //Arrange
            trailServiceMock.Setup(x => x.ListAsync()).ReturnsAsync(() =>
            {
                var trails = new List<Trail>
                {
                    new Trail
                    {
                        Id = Guid.Parse("08489d16-d7b1-4a90-8bef-0b4c94b50fe0"),
                        Name = "Sample Trail",
                        Description = "Sample trail description text...",
                        Length = 8.4,
                        Difficulty = "Hard",
                        Type = "Loop",
                        Traffick = "Moderate",
                        Attractions = "Waterfall, overlook",
                        Suitabilities = "Dog friendly",
                        Trailhead = new Trailhead
                        {
                            Id = Guid.Parse("3059e102-3aaa-49db-a991-abbd79b29140"),
                            TrailId = Guid.Parse("08489d16-d7b1-4a90-8bef-0b4c94b50fe0"),
                            Street = "123 Sample St.",
                            Street2 = null,
                            City = "Chattanooga",
                            County = "Hamilton",
                            State = "TN",
                            PostalCode = "37411",
                            Latitude = null,
                            Longitude = null,
                            Altitude = 0
                        },
                        Image = "assets/sampleTrail.jpg",
                        Photos = new List<TrailPhoto>(),
                        Events = new List<Event>(),
                        CreatedBy = null,
                        UpdatedBy = null,
                        DateCreated = DateTime.Parse("0001-01-01T00:00:00-04:57"),
                        DateModified = null,
                        IsActive = true
                    },
                };

                return trails;
            });

            var request = new List.Query();
            var cancellationToken = new CancellationToken();

            //Act
            List<Trail> trails = await listHandler.Handle(request, cancellationToken);

            //Assert
            trailServiceMock.Verify(x => x.ListAsync(), Times.Once);
        }
    }
}