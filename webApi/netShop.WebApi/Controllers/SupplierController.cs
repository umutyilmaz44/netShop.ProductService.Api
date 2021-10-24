using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using netShop.Application.Dtos;
using netShop.Application.Features.Commands.SupplierCommands;
using netShop.Application.Features.Queries.SupplierQueries;
using netShop.Application.Parameters;
using netShop.Application.Wrappers;
using netShop.WebApi.Controllers.Base;

namespace netShop.WebApi.Controllers
{
    //[Authorize]
    public class SuppliersController : BaseController
    {
        [HttpPost]
        // [AllowAnonymous]
        public async Task<ActionResult<PagedResponse<List<SupplierDto>>>> Find([FromBody] FindSuppliersQuery request, [FromQuery] int page = 0, int size = 10)
        {
            request.Page = page;
            request.PageSize = size;
            var vm = await Mediator.Send(request);

            return base.Ok(vm);
        }

        [HttpGet("{id}")]
        // [AllowAnonymous]
        public async Task<ActionResult<SupplierDto>> Get(Guid id)
        {
            var vm = await Mediator.Send(new GetSupplierDetailQuery { Id = id });

            return base.Ok(vm);
        }

        [HttpPost]
        public async Task<ActionResult<Response<SupplierDto>>> Create([FromBody] CreateSupplierCommand request)
        {
            Response<SupplierDto> response = await Mediator.Send(request);

            return Ok(response);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Update([FromBody] UpdateSupplierCommand request)
        {
            await Mediator.Send(request);

            return NoContent();
        }

        [HttpPatch("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Patch(Guid id, [FromBody] JsonPatchDocument<SupplierDto> patchDto)
        {
            PatchSupplierCommand request = new PatchSupplierCommand(id, patchDto, ModelState);
            await Mediator.Send(request);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Delete(Guid id)
        {
            await Mediator.Send(new DeleteSupplierCommand { Id = id });

            return NoContent();
        }
    }
}