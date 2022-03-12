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

namespace NetShop.ProductService.Application.Features.Queries.SupplierQueries
{
    public class FindSuppliersQuery : IRequest<PagedResponse<List<SupplierDto>>>
    {
        public string SupplierName { get; set; }
        public string Description { get; set; }
        public string Website { get; set; }
        public string Logo { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
 
        public string GenericQuery { get; set; }

        [JsonIgnore]
        public Int32? Page { get; set; } 
        [JsonIgnore]  
        public Int32? PageSize { get; set; }   
        [JsonIgnore]  
        public string Sort { get; set; } 
    }
}