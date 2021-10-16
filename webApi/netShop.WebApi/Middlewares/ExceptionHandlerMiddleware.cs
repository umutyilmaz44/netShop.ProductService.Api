using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using netShop.Application.Exceptions;
using netShop.Application.Wrappers;
using Newtonsoft.Json;

namespace netShop.WebApi.Middlewares
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
            Response<Unit> response;

            switch (exception)
            {
                case ValidationException validationException:
                    code = HttpStatusCode.BadRequest;
                    response = new Response<Unit>(nameof(ValidationException), validationException.Failures);

                    result = JsonConvert.SerializeObject(response);
                    break;
                case NotFoundException notFoundException:
                    code = HttpStatusCode.NotFound;
                    response = new Response<Unit>(nameof(NotFoundException), new Dictionary<string, string[]>(){
                        { "", new string[]{ notFoundException.Message } }
                    });

                    result = JsonConvert.SerializeObject(response);
                    break;
                case BadRequestException badRequestException:
                    code = HttpStatusCode.BadRequest;
                    response = new Response<Unit>(nameof(BadRequestException), new Dictionary<string, string[]>(){
                        { "", new string[]{ badRequestException.Message } }
                    });

                    result = JsonConvert.SerializeObject(response);
                    break;
                default:
                    code = HttpStatusCode.BadRequest;
                    response = new Response<Unit>(nameof(Exception), new Dictionary<string, string[]>(){
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