using System;

namespace Domain.Entities
{
    public class EventPhoto : DbMetadata
    {
        public Guid Id { get; set; }
        public Guid EventId { get; set; }
        public string Subject { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public EventPhotoGps GPS { get; set; }
        public string Photographer { get; set; }
    }
}