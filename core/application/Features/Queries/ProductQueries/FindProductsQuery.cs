using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LinqKit;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using NetShop.ProductService.Application.Dtos;
using NetShop.ProductService.Application.Interfaces.Repository.Base;
using NetShop.ProductService.Application.Wrappers;
using NetShop.ProductService.Domain.Entities;
using System.Text.Json.Serialization;

namespace NetShop.ProductService.Application.Features.Queries.ProductQueries
{
    public class FindProductsQuery : IRequest<PagedResponse<List<ProductDto>>>
    {
        public Guid SupplierId { get; set; }
        public Guid BrandModelId { get; set; }
        public String ProductCode { get; set; }
        public String ProductName { get; set; }
        public String Description { get; set; }

        public Double? Price { get; set; }
        public Double? PriceLowerThan { get; set; }
        public Double? PriceGreaterThan { get; set; }

        public Int32? Quantity { get; set; }     
        public Int32? QuantityLowerThan { get; set; }
        public Int32? QuantityGreaterThan { get; set; }   

        [JsonIgnore]
        public Int32? Page { get; set; } 
        [JsonIgnore]  
        public Int32? PageSize { get; set; }   
        [JsonIgnore]  
        public string Sort { get; set; } 
    }
}