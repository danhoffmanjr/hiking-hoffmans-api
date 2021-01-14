using System;

namespace Domain.Entities
{
    public class DbMetadata
    {
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public DateTimeOffset? DateModified { get; set; }
        public bool IsActive { get; set; } = true;
    }
}