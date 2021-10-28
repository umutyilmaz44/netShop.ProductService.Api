using System;
using System.Collections.Generic;
using System.Text;
using NetShop.ProductService.Domain.Common;

namespace NetShop.ProductService.Domain.Entities 
{
    public class Product : AuditableEntity {
        public Guid supplierId { get; set; }
        public Guid brandModelId { get; set; }
        public string productCode { get; set; }
        public string productName { get; set; }
        public string description { get; set; }

        public double price { get; set; }

        public int quantity { get; set; }

        public Supplier supplier { get; set; }
        public BrandModel brandModel { get; set; }

        public Product()
        {
            
        }
        public Product (Guid id, Guid supplierId, Guid brandModelId, string productCode, string productName, string description, double price, int quantity) {
            this.Id = id;
            this.supplierId = supplierId;
            this.brandModelId = brandModelId;
            this.productCode = productCode;
            this.productName = productName;
            this.description = description;
            this.price = price;
            this.quantity = quantity;
        }
    }
}