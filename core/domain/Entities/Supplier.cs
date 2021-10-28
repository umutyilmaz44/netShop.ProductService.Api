using System;
using NetShop.ProductService.Domain.Common;

namespace NetShop.ProductService.Domain.Entities
{
    public class Supplier : AuditableEntity {
        public string supplierName { get; set; }
        public string description { get; set; }
        public string website { get; set; }
        public string logo { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string fax { get; set; }


        public Supplier()
        {
            
        }

        public Supplier(Guid id, string supplierName, string description, string website, string logo, string email, string phone, string fax)
        {
            this.Id = id;
            this.supplierName = supplierName;
            this.description = description;
            this.website = website;
            this.logo = logo;
            this.email = email;
            this.phone = phone;
            this.fax = fax;
        }
    }
}