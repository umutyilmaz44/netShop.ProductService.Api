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

namespace netShop.Application.Features.Commands.SupplierCommands
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
                    throw ex;
                }
                finally
                {
                    await transaction.RollbackAsync();
                }
            }

            return new Response<SupplierDto>(this.mapper.Map<SupplierDto>(entity));
        }
    }
}