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

namespace netShop.Application.Features.Commands
{
    public class UpdateProductCommand : BaseEntity, IRequest<Response<Unit>>
    {
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }

        public double Price { get; set; }

        public int Quantity { get; set; }
    }
}