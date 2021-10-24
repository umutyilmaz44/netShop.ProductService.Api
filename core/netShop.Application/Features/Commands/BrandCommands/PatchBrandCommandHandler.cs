using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using netShop.Application.Dtos;
using netShop.Application.Exceptions;
using netShop.Application.Interfaces.Repository.Base;
using netShop.Application.Validators;
using netShop.Application.Wrappers;
using netShop.Domain.Entities;

namespace netShop.Application.Features.Commands.BrandCommands
{
    public class PatchBrandCommandHandler : IRequestHandler<PatchBrandCommand, Response<Unit>>
    {
        IMapper mapper;
        IUnitOfWork unitOfWork;

        public PatchBrandCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Response<Unit>> Handle(PatchBrandCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new BadRequestException($"{nameof(PatchBrandCommand)} request is null");
            }

            Brand entity = await this.unitOfWork.brandRepository.GetByIdAsync(request.Id);
            if (entity == null)
            {
                throw new NotFoundException(nameof(Brand), request.Id);
            }
            BrandDto dto = this.mapper.Map<BrandDto>(entity);
            request.patchDto.ApplyTo(dto, request.modelState);
            if (!request.modelState.IsValid)
            {
                throw new BadRequestException($"{nameof(Brand)} Model path error");
            }
            
            entity = this.mapper.Map<Brand>(dto);
            await this.unitOfWork.brandRepository.UpdateAsync(entity);
            await this.unitOfWork.CommitAsync();

            return new Response<Unit>(Unit.Value);
        }
    }
}