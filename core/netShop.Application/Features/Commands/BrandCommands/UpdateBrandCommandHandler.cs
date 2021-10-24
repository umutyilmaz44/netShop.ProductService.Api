using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using netShop.Application.Dtos;
using netShop.Application.Exceptions;
using netShop.Application.Interfaces.Repository.Base;
using netShop.Application.Validators;
using netShop.Application.Wrappers;
using netShop.Domain.Entities;

namespace netShop.Application.Features.Commands.BrandCommands
{
    public class UpdateBrandCommandHandler : IRequestHandler<UpdateBrandCommand, Response<Unit>>
    {
        IMapper mapper;
        IUnitOfWork unitOfWork;

        public UpdateBrandCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Response<Unit>> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new BadRequestException($"{nameof(UpdateBrandCommand)} request is null");
            }

            Brand entity = await this.unitOfWork.brandRepository.GetByIdAsync(request.Id);
            if (entity == null)
            {
                throw new NotFoundException(nameof(Brand), request.Id);
            }
            
            await this.unitOfWork.brandRepository.UpdateAsync(this.mapper.Map<Brand>(request));
            await this.unitOfWork.CommitAsync();

            return new Response<Unit>(Unit.Value);
        }
    }
}