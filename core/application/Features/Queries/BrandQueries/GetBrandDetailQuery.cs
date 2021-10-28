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

namespace NetShop.ProductService.Application.Features.Queries.BrandQueries
{
    public class GetBrandDetailQuery : IRequest<Response<BrandDto>>
    {
        public Guid Id { get; set; }
        public string BrandName { get; set; }
        public string Description { get; set; }
    }
}