using System;

namespace Domain.Entities
{
    public class TrailPhoto
    {
        public Guid Id { get; set; }
        public Guid TrailId { get; set; }
        public string Url { get; set; }
        public bool IsMain { get; set; }
    }
}