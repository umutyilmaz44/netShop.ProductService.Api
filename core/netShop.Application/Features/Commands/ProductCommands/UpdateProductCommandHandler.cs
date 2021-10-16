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

namespace netShop.Application.Features.Commands.ProductCommands
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Response<Unit>>
    {
        IMapper mapper;
        IUnitOfWork unitOfWork;

        public UpdateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Response<Unit>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            Product entity = await this.unitOfWork.productRepository.GetByIdAsync(request.Id);
            if (entity == null)
            {
                throw new NotFoundException(nameof(Product), request.Id);
            }
            
            await this.unitOfWork.productRepository.UpdateAsync(this.mapper.Map<Product>(request));
            await this.unitOfWork.CommitAsync();

            return new Response<Unit>(Unit.Value);
        }
    }
}