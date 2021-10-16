using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using netShop.Application.Dtos;
using netShop.Application.Features.Commands;
using netShop.Application.Features.Queries;
using netShop.Application.Parameters;
using netShop.Application.Wrappers;
using netShop.WebApi.Controllers.Base;

namespace netShop.WebApi.Controllers
{
    //[Authorize]
    public class ProductsController : BaseController
    {
        [HttpPost]
        // [AllowAnonymous]
        public async Task<ActionResult<PagedResponse<List<ProductDto>>>> Find([FromBody] FindProductsQuery request, [FromQuery] int page=0, int size=10)
        {
            var vm = await Mediator.Send(request);

            return base.Ok(vm);
        }

        [HttpGet("{id}")]
        // [AllowAnonymous]
        public async Task<ActionResult<ProductDto>> Get(Guid id)
        {
            var vm = await Mediator.Send(new GetProductDetailQuery { Id = id });

            return base.Ok(vm);
        }

        [HttpPost]
        public async Task<ActionResult<Response<ProductDto>>> Create([FromBody] CreateProductCommand request)
        {
            Response<ProductDto> response = await Mediator.Send(request);

            return Ok(response);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Update([FromBody] UpdateProductCommand request)
        {
            await Mediator.Send(request);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Delete(Guid id)
        {
            await Mediator.Send(new DeleteProductCommand { Id = id });

            return NoContent();
        }        
    }
}