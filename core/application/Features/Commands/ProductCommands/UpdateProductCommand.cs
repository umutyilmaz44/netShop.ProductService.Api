using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using NetShop.ProductService.Application.Dtos;
using NetShop.ProductService.Application.Interfaces.Repository.Base;
using NetShop.ProductService.Application.Wrappers;
using NetShop.ProductService.Domain.Common;
using NetShop.ProductService.Domain.Entities;

namespace NetShop.ProductService.Application.Features.Commands.ProductCommands
{
    public class UpdateProductCommand : BaseEntity, IRequest<Response>
    {
        public string SupplierId { get; set; }
        public string BrandModelId { get; set; }
        public String ProductCode { get; set; }
        public String ProductName { get; set; }
        public String Description { get; set; }
        public Double Price { get; set; }
        public Int32 Quantity { get; set; }
    }
}