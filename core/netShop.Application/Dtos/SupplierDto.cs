using System;
using System.Collections.Generic;
using System.Text;
using netShop.Domain.Common;

namespace netShop.Application.Dtos
{
    public class SupplierDto : BaseEntity
    {
        public string supplierName { get; set; }
        public string description { get; set; }
        public string website { get; set; }
        public string logo { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string fax { get; set; }

        public SupplierDto()
        {

        }
        public SupplierDto(Guid id, string supplierName, string description, string website, string logo, string email, string phone, string fax)
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