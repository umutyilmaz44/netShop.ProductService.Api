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
using NetShop.ProductService.Domain.Entities;

namespace NetShop.ProductService.Application.Features.Commands.SupplierCommands
{
    public class CreateSupplierCommandHandler : IRequestHandler<CreateSupplierCommand, Response<SupplierDto>>
    {
        IMapper mapper;
        IUnitOfWork unitOfWork;

        public CreateSupplierCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }
        public async Task<Response<SupplierDto>> Handle(CreateSupplierCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new BadRequestException($"{nameof(CreateSupplierCommand)} request is null");
            }

            Supplier entity = null;

            using (var transaction = await this.unitOfWork.BeginTransactionAsync())
            {
                try
                {
                    entity = await this.unitOfWork.supplierRepository.AddAsync(this.mapper.Map<Supplier>(request));
                    await this.unitOfWork.CommitAsync();

                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw ex;
                }
            }

            return new Response<SupplierDto>(this.mapper.Map<SupplierDto>(entity));
        }
    }
}