using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using NetShop.ProductService.Application.Dtos;
using NetShop.ProductService.Application.Interfaces.Repository.Base;
using NetShop.ProductService.Application.Wrappers;
using NetShop.ProductService.Domain.Entities;

namespace NetShop.ProductService.Application.Features.Queries.BrandModelQueries
{
    public class GetBrandModelDetailQuery : IRequest<Response<BrandModelDto>>
    {
        public Guid Id { get; set; }
        public Guid BrandId { get; set; }
        public string ModelName { get; set; }
        public string Description { get; set; }
    }
}