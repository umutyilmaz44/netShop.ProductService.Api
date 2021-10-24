using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using netShop.Application.Dtos;
using netShop.Application.Interfaces.Repository.Base;
using netShop.Application.Wrappers;
using netShop.Domain.Entities;

namespace netShop.Application.Features.Commands.BrandModelCommands
{
    public class CreateBrandModelCommand : IRequest<Response<BrandModelDto>>
    {
        public Guid BrandId { get; set; }
        public string ModelName { get; set; }
        public string Description { get; set; }
    }
}