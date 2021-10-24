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

namespace netShop.Application.Features.Commands.SupplierCommands
{
    public class DeleteSupplierCommandHandler : IRequestHandler<DeleteSupplierCommand, Response<Unit>>
    {
        IMapper mapper;
        IUnitOfWork unitOfWork;

        public DeleteSupplierCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Response<Unit>> Handle(DeleteSupplierCommand request, CancellationToken cancellationToken)
        {
            if (request == null || request.Id == Guid.Empty)
            {
                throw new BadRequestException($"{nameof(DeleteSupplierCommand)} request is null");
            }

            Supplier entity = await this.unitOfWork.supplierRepository.GetByIdAsync(request.Id);
            if (entity == null)
            {
                throw new NotFoundException(nameof(Supplier), request.Id);
            }

            await this.unitOfWork.supplierRepository.DeleteAsync(entity);
            await this.unitOfWork.CommitAsync();
            return new Response<Unit>(Unit.Value);
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}