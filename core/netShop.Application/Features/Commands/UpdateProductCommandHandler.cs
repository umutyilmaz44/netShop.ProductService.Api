using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using netShop.Application.Dtos;
using netShop.Application.Interfaces.Repository.Base;
using netShop.Application.Validators;
using netShop.Application.Wrappers;
using netShop.Domain.Entities;

namespace netShop.Application.Features.Commands
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
            UpdateProductCommandValidator validator = new UpdateProductCommandValidator();
            var validationResult = validator.Validate(request);
            if (!validationResult.IsValid)
            {
                return new Response<Unit>("Validation Error", validationResult.Errors.Select(x => $"{x.ErrorCode} : {x.ErrorMessage}").ToArray());
            }

            await this.unitOfWork.productRepository.UpdateAsync(this.mapper.Map<Product>(request));
            await this.unitOfWork.CommitAsync();

            return new Response<Unit>(Unit.Value);
        }
    }
}