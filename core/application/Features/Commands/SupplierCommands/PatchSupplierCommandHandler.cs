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

namespace NetShop.ProductService.Application.Features.Commands.SupplierCommands
{
    public class PatchSupplierCommandHandler : IRequestHandler<PatchSupplierCommand, Response>
    {
        IMapper mapper;
        IUnitOfWork unitOfWork;

        public PatchSupplierCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Response> Handle(PatchSupplierCommand request, CancellationToken cancellationToken)
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

            return new Response(true);
        }
    }
}