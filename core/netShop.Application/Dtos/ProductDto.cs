using System;
using System.Collections.Generic;
using System.Text;

namespace netShop.Application.Dtos
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string productCode { get; set; }
        public string productName { get; set; }
        public string description { get; set; }

        public double price { get; set; }

        public int quantity { get; set; }

        public ProductDto()
        {

        }
        public ProductDto(Guid id, string GetProductByCode, string productName, string description, double price, int quantity)
        {
            this.Id = id;
            this.productCode = productCode;
            this.productName = productName;
            this.description = description;
            this.price = price;
            this.quantity = quantity;

        }
    }
}