using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using NetShop.ProductService.Application.Dtos;
using NetShop.ProductService.Application.Exceptions;
using NetShop.ProductService.Application.Interfaces.Repository.Base;
using NetShop.ProductService.Application.Validators;
using NetShop.ProductService.Application.Wrappers;
using NetShop.ProductService.Domain.Entities;

namespace NetShop.ProductService.Application.Features.Commands.BrandModelCommands
{
    public class PatchBrandModelCommandHandler : IRequestHandler<PatchBrandModelCommand, Response>
    {
        IMapper mapper;
        IUnitOfWork unitOfWork;

        public PatchBrandModelCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Response> Handle(PatchBrandModelCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new BadRequestException($"{nameof(PatchBrandModelCommand)} request is null");
            }

            BrandModel entity = await this.unitOfWork.brandModelRepository.GetByIdAsync(request.Id);
            if (entity == null)
            {
                throw new NotFoundException(nameof(BrandModel), request.Id);
            }
            BrandModelDto dto = this.mapper.Map<BrandModelDto>(entity);
            request.patchDto.ApplyTo(dto, request.modelState);
            if (!request.modelState.IsValid)
            {
                throw new BadRequestException($"{nameof(BrandModel)} Model path error");
            }
            
            entity = this.mapper.Map<BrandModel>(dto);
            await this.unitOfWork.brandModelRepository.UpdateAsync(entity);
            await this.unitOfWork.CommitAsync();

            return new Response(true);
        }
    }
}