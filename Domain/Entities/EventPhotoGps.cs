using System;

namespace Domain.Entities
{
    public class EventPhotoGps  : BaseEntity
    {
        public Guid EventPhotoId { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public int Altitude { get; set; }
    }
}