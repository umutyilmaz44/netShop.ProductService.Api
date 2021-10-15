using System;
using System.Collections.Generic;
using System.Text;

namespace netShop.Domain.Common
{
    public abstract class AuditableEntity : BaseEntity
    {
        public DateTime CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? LastModifiedDate { get; set; }

        public string LastModifiedBy { get; set; }
    }
}