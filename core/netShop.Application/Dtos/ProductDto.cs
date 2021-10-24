using System;
using System.Collections.Generic;
using System.Text;
using netShop.Domain.Common;

namespace netShop.Application.Dtos
{
    public class ProductDto : BaseEntity
    {
        public Guid supplierId { get; set; }
        public Guid brandModelId { get; set; }
        public string productCode { get; set; }
        public string productName { get; set; }
        public string description { get; set; }
        public double price { get; set; }
        public int quantity { get; set; }
        public SupplierDto supplier { get; set; }
        public BrandModelDto brandModel { get; set; }

        public ProductDto()
        {

        }
        public ProductDto(Guid id, Guid supplierId, Guid brandModelId, string GetProductByCode, string productName, string description, double price, int quantity)
        {
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