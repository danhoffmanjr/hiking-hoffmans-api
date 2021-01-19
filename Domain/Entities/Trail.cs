using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Trail : DbMetadata
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Length { get; set; }
        public string Difficulty { get; set; }
        public string Type { get; set; }
        public string Traffick { get; set; }
        public string Attractions { get; set; }
        public string Suitabilities { get; set; }
        public Trailhead Trailhead { get; set; }
        public string Image { get; set; }
        public ICollection<TrailPhoto> Photos { get; set; }
        public ICollection<Event> Events { get; set; }
    }
}