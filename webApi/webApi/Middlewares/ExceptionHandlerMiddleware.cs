using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using NetShop.ProductService.Application.Exceptions;
using NetShop.ProductService.Application.Wrappers;
using Newtonsoft.Json;

namespace NetShop.ProductService.WebApi.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;

            var result = string.Empty;
            Response response;

            switch (exception)
            {
                case ValidationException validationException:
                    code = HttpStatusCode.BadRequest;
                    response = new Response(nameof(ValidationException), validationException.Failures);

                    result = JsonConvert.SerializeObject(response);
                    break;
                case NotFoundException notFoundException:
                    code = HttpStatusCode.NotFound;
                    response = new Response(nameof(NotFoundException), new Dictionary<string, string[]>(){
                        { "", new string[]{ notFoundException.Message } }
                    });

                    result = JsonConvert.SerializeObject(response);
                    break;
                case BadRequestException badRequestException:
                    code = HttpStatusCode.BadRequest;
                    response = new Response(nameof(BadRequestException), new Dictionary<string, string[]>(){
                        { "", new string[]{ badRequestException.Message } }
                    });

                    result = JsonConvert.SerializeObject(response);
                    break;
                default:
                    code = HttpStatusCode.BadRequest;
                    response = new Response(nameof(Exception), new Dictionary<string, string[]>(){
                        { "", new string[]{ exception.Message } }
                    });

                    result = JsonConvert.SerializeObject(response);
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            if (result == string.Empty)
            {
                result = JsonConvert.SerializeObject(new { error = exception.Message });
            }

            return context.Response.WriteAsync(result);
        }
    }

    public static class ExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}