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

namespace netShop.Application.Features.Commands.SupplierCommands
{
    public class PatchSupplierCommandHandler : IRequestHandler<PatchSupplierCommand, Response<Unit>>
    {
        IMapper mapper;
        IUnitOfWork unitOfWork;

        public PatchSupplierCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Response<Unit>> Handle(PatchSupplierCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new BadRequestException($"{nameof(PatchSupplierCommand)} request is null");
            }

            Supplier entity = await this.unitOfWork.supplierRepository.GetByIdAsync(request.Id);
            if (entity == null)
            {
                throw new NotFoundException(nameof(Supplier), request.Id);
            }
            SupplierDto dto = this.mapper.Map<SupplierDto>(entity);
            request.patchDto.ApplyTo(dto, request.modelState);
            if (!request.modelState.IsValid)
            {
                throw new BadRequestException($"{nameof(Supplier)} Model path error");
            }
            
            entity = this.mapper.Map<Supplier>(dto);
            await this.unitOfWork.supplierRepository.UpdateAsync(entity);
            await this.unitOfWork.CommitAsync();

            return new Response<Unit>(Unit.Value);
        }
    }
}