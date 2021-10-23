using System;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using netShop.Application.Dtos;
using netShop.Application.Wrappers;
using netShop.Domain.Common;

namespace netShop.Application.Features.Commands.ProductCommands
{
    public class PatchProductCommand : BaseEntity, IRequest<Response<Unit>>
    {
        public JsonPatchDocument<ProductDto> patchDto { get; set; }
        public ModelStateDictionary modelState { get; }

        public PatchProductCommand(Guid id, JsonPatchDocument<ProductDto> patchDto, ModelStateDictionary modelState)
        {
            this.Id = id;
            this.patchDto = patchDto;
            this.modelState = modelState;
        }
    }
}