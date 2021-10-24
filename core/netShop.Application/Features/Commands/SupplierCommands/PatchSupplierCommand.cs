using System;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using netShop.Application.Dtos;
using netShop.Application.Wrappers;
using netShop.Domain.Common;

namespace netShop.Application.Features.Commands.SupplierCommands
{
    public class PatchSupplierCommand : BaseEntity, IRequest<Response<Unit>>
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