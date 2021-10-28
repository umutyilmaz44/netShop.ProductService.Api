using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using NetShop.ProductService.Application.Dtos;
using NetShop.ProductService.Application.Exceptions;
using NetShop.ProductService.Application.Interfaces.Repository.Base;
using NetShop.ProductService.Application.Validators;
using NetShop.ProductService.Application.Wrappers;
using NetShop.ProductService.Domain.Common;
using NetShop.ProductService.Domain.Entities;

namespace NetShop.ProductService.Application.Features.Commands.BrandModelCommands
{
    public class DeleteBrandModelCommandHandler : IRequestHandler<DeleteBrandModelCommand, Response>
    {
        IMapper mapper;
        IUnitOfWork unitOfWork;

        public DeleteBrandModelCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Response> Handle(DeleteBrandModelCommand request, CancellationToken cancellationToken)
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
            return new Response(true);
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}