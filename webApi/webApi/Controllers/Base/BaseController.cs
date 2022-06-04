using System.Net;
using MediatR;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using NetShop.ProductService.Domain.Common;
using Microsoft.Extensions.Logging;

namespace NetShop.ProductService.WebApi.Controllers.Base
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseController : ControllerBase
    {
        private IMediator _mediator;

        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        protected readonly ILogger<BaseController> logger;
        public BaseController(ILogger<BaseController> logger)
        {
            this.logger = logger;
        }
    }
}