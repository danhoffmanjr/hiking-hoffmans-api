using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Event : BaseEntity
    {
        public Guid TrailId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public DateTimeOffset Date { get; set; }
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset EndTime { get; set; }
        public TimeSpan Duration
        {
            get
            {
                return this.EndTime - this.StartTime;
            }
        }
        public string MapUrl { get; set; }
        public ICollection<EventPhoto> EventPhotos { get; set; }
        public WeatherCondition Weather { get; set; }
    }
}