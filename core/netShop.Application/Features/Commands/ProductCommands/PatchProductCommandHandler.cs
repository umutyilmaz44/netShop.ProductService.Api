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

namespace netShop.Application.Features.Commands.ProductCommands
{
    public class PatchProductCommandHandler : IRequestHandler<PatchProductCommand, Response<Unit>>
    {
        IMapper mapper;
        IUnitOfWork unitOfWork;

        public PatchProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Response<Unit>> Handle(PatchProductCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new BadRequestException($"{nameof(PatchProductCommand)} request is null");
            }

            Product entity = await this.unitOfWork.productRepository.GetByIdAsync(request.Id);
            if (entity == null)
            {
                throw new NotFoundException(nameof(Product), request.Id);
            }
            ProductDto dto = this.mapper.Map<ProductDto>(entity);
            request.patchDto.ApplyTo(dto, request.modelState);
            if (!request.modelState.IsValid)
            {
                throw new BadRequestException($"{nameof(Product)} Model path error");
            }
            
            entity = this.mapper.Map<Product>(dto);
            await this.unitOfWork.productRepository.UpdateAsync(entity);
            await this.unitOfWork.CommitAsync();

            return new Response<Unit>(Unit.Value);
        }
    }
}