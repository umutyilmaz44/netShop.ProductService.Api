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

namespace netShop.Application.Features.Commands.BrandModelCommands
{
    public class DeleteBrandModelCommandHandler : IRequestHandler<DeleteBrandModelCommand, Response<Unit>>
    {
        IMapper mapper;
        IUnitOfWork unitOfWork;

        public DeleteBrandModelCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Response<Unit>> Handle(DeleteBrandModelCommand request, CancellationToken cancellationToken)
        {
            if (request == null || request.Id == Guid.Empty)
            {
                throw new BadRequestException($"{nameof(DeleteBrandModelCommand)} request is null");
            }

            BrandModel entity = await this.unitOfWork.brandModelRepository.GetByIdAsync(request.Id);
            if (entity == null)
            {
                throw new NotFoundException(nameof(BrandModel), request.Id);
            }

            await this.unitOfWork.brandModelRepository.DeleteAsync(entity);
            await this.unitOfWork.CommitAsync();
            return new Response<Unit>(Unit.Value);
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}