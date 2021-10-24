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
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Response<ProductDto>>
    {
        IMapper mapper;
        IUnitOfWork unitOfWork;

        public CreateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }
        public async Task<Response<ProductDto>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new BadRequestException($"{nameof(CreateProductCommand)} request is null");
            }

            Product entity = null;

            using (var transaction = await this.unitOfWork.BeginTransactionAsync())
            {
                try
                {
                    entity = await this.unitOfWork.productRepository.AddAsync(this.mapper.Map<Product>(request));
                    await this.unitOfWork.CommitAsync();

                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    await transaction.RollbackAsync();
                }
            }

            return new Response<ProductDto>(this.mapper.Map<ProductDto>(entity));
        }
    }
}