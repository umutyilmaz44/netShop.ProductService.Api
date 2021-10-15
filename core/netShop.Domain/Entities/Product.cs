using System;
using System.Collections.Generic;
using System.Text;
using netShop.Domain.Common;

namespace netShop.Domain.Entities 
{
    public class Product : AuditableEntity {
        public string productCode { get; set; }
        public string productName { get; set; }
        public string description { get; set; }

        public double price { get; set; }

        public int quantity { get; set; }

        public Product (Guid id, string productCode, string productName, string description, double price, int quantity) {
            this.Id = id;
            this.productCode = productCode;
            this.productName = productName;
            this.description = description;
            this.price = price;
            this.quantity = quantity;

        }
    }
}