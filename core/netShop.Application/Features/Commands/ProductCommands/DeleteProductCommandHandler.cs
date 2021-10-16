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

namespace netShop.Application.Features.Commands.ProductCommands
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Response<Unit>>
    {
        IMapper mapper;
        IUnitOfWork unitOfWork;

        public DeleteProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Response<Unit>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            Product entity = await this.unitOfWork.productRepository.GetByIdAsync(request.Id);
            if (entity == null)
            {
                throw new NotFoundException(nameof(Product), request.Id);
            }

            await this.unitOfWork.productRepository.DeleteAsync(this.mapper.Map<Product>(entity));
            await this.unitOfWork.CommitAsync();
            return new Response<Unit>(Unit.Value);
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}