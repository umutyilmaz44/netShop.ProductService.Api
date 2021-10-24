using System;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using netShop.Application.Dtos;
using netShop.Application.Wrappers;
using netShop.Domain.Common;

namespace netShop.Application.Features.Commands.BrandModelCommands
{
    public class PatchBrandModelCommand : BaseEntity, IRequest<Response<Unit>>
    {
        public JsonPatchDocument<BrandModelDto> patchDto { get; set; }
        public ModelStateDictionary modelState { get; }

        public PatchBrandModelCommand(Guid id, JsonPatchDocument<BrandModelDto> patchDto, ModelStateDictionary modelState)
        {
            this.Id = id;
            this.patchDto = patchDto;
            this.modelState = modelState;
        }
    }
}