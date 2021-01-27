using System;

namespace Domain.Entities
{
    public class TrailheadLocation : BaseEntity
    {
        public Guid TrailId { get; set; }
        public string Country { get; set; } = "United States";
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
                if (this.Country != "United States")
                {
                    return this.InternationalLocation.Address;
                }

                return $@"{this.Street} {(String.IsNullOrEmpty(this.Street2) ? String.Empty : this.Street2)} {this.City}, {this.State} {this.PostalCode}";
            }
        }

        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public int? Altitude { get; set; }
        public InternationalAddress InternationalLocation { get; set; } = null;
    }
}