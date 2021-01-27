using System;

namespace Domain.Entities
{
    public class TrailPhoto : BaseEntity
    {
        public Guid TrailId { get; set; }
        public string Url { get; set; }
        public bool IsMain { get; set; }
    }
}