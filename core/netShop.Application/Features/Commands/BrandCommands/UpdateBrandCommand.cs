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

namespace netShop.Application.Features.Commands.BrandCommands
{
    public class UpdateBrandCommand : BaseEntity, IRequest<Response<Unit>>
    {
        public string BrandName { get; set; }
        public string Description { get; set; }
    }
}