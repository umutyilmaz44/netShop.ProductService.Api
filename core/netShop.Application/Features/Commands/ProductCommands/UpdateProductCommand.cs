using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using netShop.Application.Dtos;
using netShop.Application.Interfaces.Repository.Base;
using netShop.Application.Wrappers;
using netShop.Domain.Common;
using netShop.Domain.Entities;

namespace netShop.Application.Features.Commands.ProductCommands
{
    public class UpdateProductCommand : BaseEntity, IRequest<Response<Unit>>
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