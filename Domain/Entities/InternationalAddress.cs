using System;

namespace Domain.Entities
{
    public class InternationalAddress
    {
        public Guid Id { get; set; }
        public Guid TrailheadLocationId { get; set; }
        public string Address { get; set; }
    }
}