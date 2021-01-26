using System;
using System.Collections.Generic;
using System.Linq;
using Application.Trails;
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

        public static readonly Create.Query Trail_CreateQuery_US_Mock = new Create.Query
        {
            Name = "Created Trail",
            Description = "string",
            Length = 7.1,
            Difficulty = "string",
            Type = "string",
            Traffick = "string",
            Attractions = "string",
            Suitabilities = "string",
            Trailhead = new TrailheadLocation
            {
                Country = "United States",
                Street = "string",
                Street2 = null,
                City = "string",
                County = "string",
                State = "string",
                PostalCode = "string",
                Latitude = "string",
                Longitude = "string",
                Altitude = 100,
                InternationalLocation = null
            },
            Image = "string",
        };

        public static readonly Trail CreateTrail_US_Mock = new Trail
        {
            Id = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
            Name = "Created Trail",
            Description = "string",
            Length = 7.1,
            Difficulty = "string",
            Type = "string",
            Traffick = "string",
            Attractions = "string",
            Suitabilities = "string",
            Trailhead = new TrailheadLocation
            {
                Id = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                TrailId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa7"),
                Country = "United States",
                Street = "string",
                Street2 = null,
                City = "string",
                County = "string",
                State = "string",
                PostalCode = "string",
                Latitude = "string",
                Longitude = "string",
                Altitude = 100,
                InternationalLocation = null
            },
            Image = "string",
            CreatedBy = Guid.NewGuid().ToString(),
            DateCreated = DateTime.UtcNow,
            IsActive = true
        };

        public static readonly Trail CreateTrail_International_Mock = new Trail
        {
            Id = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
            Name = "Created Trail",
            Description = "string",
            Length = 7.1,
            Difficulty = "string",
            Type = "string",
            Traffick = "string",
            Attractions = "string",
            Suitabilities = "string",
            Trailhead = new TrailheadLocation
            {
                Id = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                TrailId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa7"),
                Country = "France",
                Street = "string",
                Street2 = null,
                City = "string",
                County = "string",
                State = "string",
                PostalCode = "string",
                Latitude = "string",
                Longitude = "string",
                Altitude = 100,
                InternationalLocation = new InternationalAddress
                {
                    Id = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                    TrailheadLocationId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                    Address = "Champ de Mars, 5 Avenue Anatole France, 75007 Paris"
                }
            },
            Image = "string",
            CreatedBy = Guid.NewGuid().ToString(),
            DateCreated = DateTime.UtcNow,
            IsActive = true
        };
    }
}