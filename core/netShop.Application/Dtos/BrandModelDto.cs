using System;
using System.Collections.Generic;
using System.Text;
using netShop.Domain.Common;

namespace netShop.Application.Dtos
{
    public class BrandModelDto : BaseEntity
    {
         public Guid brandId { get; set; }
        public string modelName { get; set; }
        public string description { get; set; }

        public BrandDto brand { get; set; }

        public BrandModelDto()
        {

        }
        public BrandModelDto(Guid id, Guid brandId, string modelName, string description) {
            this.Id = id;
            this.brandId = brandId;
            this.modelName = modelName;
            this.description = description;
        }
    }
}