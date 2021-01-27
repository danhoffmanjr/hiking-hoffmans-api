using System;

namespace Domain.Entities
{
    public class InternationalAddress : BaseEntity
    {
        public Guid TrailheadLocationId { get; set; }
        public string Address { get; set; }
    }
}