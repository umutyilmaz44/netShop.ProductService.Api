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

namespace netShop.Application.Features.Queries.BrandModelQueries
{
    public class FindBrandModelsQuery : IRequest<PagedResponse<List<BrandModelDto>>>
    {
        public Guid BrandId { get; set; }
        public string ModelName { get; set; }
        public string Description { get; set; }

        [JsonIgnore]
        public Int32? Page { get; set; } 
        [JsonIgnore]  
        public Int32? PageSize { get; set; }   
    }
}