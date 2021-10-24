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
using netShop.Domain.Common;
using netShop.Domain.Entities;

namespace netShop.Application.Features.Commands.BrandCommands
{
    public class DeleteBrandCommandHandler : IRequestHandler<DeleteBrandCommand, Response<Unit>>
    {
        IMapper mapper;
        IUnitOfWork unitOfWork;

        public DeleteBrandCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Response<Unit>> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
        {
            if (request == null || request.Id == Guid.Empty)
            {
                throw new BadRequestException($"{nameof(DeleteBrandCommand)} request is null");
            }

            Brand entity = await this.unitOfWork.brandRepository.GetByIdAsync(request.Id);
            if (entity == null)
            {
                throw new NotFoundException(nameof(Brand), request.Id);
            }

            await this.unitOfWork.brandRepository.DeleteAsync(entity);
            await this.unitOfWork.CommitAsync();
            return new Response<Unit>(Unit.Value);
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}