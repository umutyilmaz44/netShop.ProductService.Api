using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LinqKit;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using netShop.Application.Dtos;
using netShop.Application.Interfaces.Repository.Base;
using netShop.Application.Wrappers;
using netShop.Domain.Entities;
using System.Text.Json.Serialization;

namespace netShop.Application.Features.Queries.ProductQueries
{
    public class FindProductsQuery : IRequest<PagedResponse<List<ProductDto>>>
    {
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
    }
}