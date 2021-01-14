using System;

namespace Domain.Entities
{
    public class Trailhead
    {
        public Guid Id { get; set; }
        public Guid TrailId { get; set; }
        public string Street { get; set; }
        public string Street2 { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Address
        {
            get
            {
                return $@"{this.Street} {(String.IsNullOrEmpty(this.Street2) ? String.Empty : this.Street2)} {this.City}, {this.State} {this.PostalCode}";
            }
        }

        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public int Altitude { get; set; }
    }
}