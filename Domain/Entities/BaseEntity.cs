using System;
using Domain.Interfaces;

namespace Domain.Entities
{
    public class BaseEntity : IEntity
    {
        public Guid Id { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public DateTimeOffset? DateModified { get; set; }
        public bool IsActive { get; set; } = true;
    }
}