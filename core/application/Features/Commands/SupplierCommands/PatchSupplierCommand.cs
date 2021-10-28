using System;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NetShop.ProductService.Application.Dtos;
using NetShop.ProductService.Application.Wrappers;
using NetShop.ProductService.Domain.Common;

namespace NetShop.ProductService.Application.Features.Commands.SupplierCommands
{
    public class PatchSupplierCommand : BaseEntity, IRequest<Response>
    {
        public JsonPatchDocument<SupplierDto> patchDto { get; set; }
        public ModelStateDictionary modelState { get; }

        public PatchSupplierCommand(Guid id, JsonPatchDocument<SupplierDto> patchDto, ModelStateDictionary modelState)
        {
            this.Id = id;
            this.patchDto = patchDto;
            this.modelState = modelState;
        }
    }
}