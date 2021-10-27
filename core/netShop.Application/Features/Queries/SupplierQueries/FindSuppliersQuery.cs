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

namespace netShop.Application.Features.Queries.SupplierQueries
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
 

        [JsonIgnore]
        public Int32? Page { get; set; } 
        [JsonIgnore]  
        public Int32? PageSize { get; set; }   
        [JsonIgnore]  
        public string Sort { get; set; } 
    }
}