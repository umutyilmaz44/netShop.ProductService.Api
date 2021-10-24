using System;
using System.Collections.Generic;
using System.Text;
using netShop.Domain.Common;

namespace netShop.Domain.Entities 
{
    public class Brand : AuditableEntity {
        public string brandName { get; set; }
        public string description { get; set; }

        public Brand()
        {
            
        }
        public Brand (Guid id, string brandName, string description) {
            this.Id = id;
            this.brandName = brandName;
            this.description = description;
        }
    }
}