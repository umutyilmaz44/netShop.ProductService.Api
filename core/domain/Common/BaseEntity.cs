using System;
using System.Collections.Generic;
using System.Text; 

namespace NetShop.ProductService.Domain.Common
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
    }
}