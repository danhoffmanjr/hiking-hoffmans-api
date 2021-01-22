using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities;

namespace Test.Trails
{
    public static class TrailsMockData
    {
        public static readonly List<Trail> TrailListMock = new List<Trail>
        {
            new Trail
            {
                Id = Guid.NewGuid(),
                Name = "Sample Trail 1",
                Description = "Sample trail one description text...",
                Length = 8.4,
                Difficulty = "Hard",
                Type = "Loop",
                Traffick = "Moderate",
                Attractions = "Waterfall, Streams",
                Suitabilities = "Dog friendly",
                Trailhead = new TrailheadLocation
                {
                    Id = Guid.NewGuid(),
                    TrailId = Guid.NewGuid(),
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
                DateCreated = DateTime.Now,
                DateModified = null,
                IsActive = true
            },
            new Trail
            {
                Id = Guid.NewGuid(),
                Name = "Sample Trail 2",
                Description = "Sample trail two description text...",
                Length = 8.4,
                Difficulty = "Easy",
                Type = "Out and Back",
                Traffick = "Heavy",
                Attractions = "Waterfalls, Overlooks",
                Suitabilities = "Dog friendly",
                Trailhead = new TrailheadLocation
                {
                    Id = Guid.NewGuid(),
                    TrailId = Guid.NewGuid(),
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
                Image = "assets/sampleTrail2.jpg",
                Photos = new List<TrailPhoto>(),
                Events = new List<Event>(),
                CreatedBy = null,
                UpdatedBy = null,
                DateCreated = DateTime.Now,
                DateModified = null,
                IsActive = true
            },
            new Trail
            {
                Id = Guid.Parse("08489d16-d7b1-4a90-8bef-0b4c94b50fe0"),
                Name = "Trail By Id",
                Description = "Sample trail by id description text...",
                Length = 8.4,
                Difficulty = "Hard",
                Type = "Loop",
                Traffick = "Moderate",
                Attractions = "Waterfall, Streams",
                Suitabilities = "Dog friendly",
                Trailhead = new TrailheadLocation
                {
                    Id = Guid.NewGuid(),
                    TrailId = Guid.NewGuid(),
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
                DateCreated = DateTime.Now,
                DateModified = null,
                IsActive = true
            }
        };

        public static readonly IQueryable<Trail> TrailListMockAsQueryable = TrailListMock.AsQueryable();

        public static readonly Trail TrailByIdMock = new Trail
        {
            Id = Guid.Parse("08489d16-d7b1-4a90-8bef-0b4c94b50fe0"),
            Name = "Trail By Id",
            Description = "Sample trail by id description text...",
            Length = 8.4,
            Difficulty = "Hard",
            Type = "Loop",
            Traffick = "Moderate",
            Attractions = "Waterfall, Streams",
            Suitabilities = "Dog friendly",
            Trailhead = new TrailheadLocation
            {
                Id = Guid.NewGuid(),
                TrailId = Guid.NewGuid(),
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
            DateCreated = DateTime.Now,
            DateModified = null,
            IsActive = true
        };
    }
}