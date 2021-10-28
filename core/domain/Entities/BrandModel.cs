using System;
using System.Collections.Generic;
using System.Text;
using NetShop.ProductService.Domain.Common;

namespace NetShop.ProductService.Domain.Entities 
{
    public class BrandModel : AuditableEntity {
        public Guid brandId { get; set; }
        public string modelName { get; set; }
        public string description { get; set; }

        public Brand brand { get; set; }

        public BrandModel()
        {
            
        }
        public BrandModel (Guid id, Guid brandId, string modelName, string description) {
            this.Id = id;
            this.brandId = brandId;
            this.modelName = modelName;
            this.description = description;
        }
    }
}