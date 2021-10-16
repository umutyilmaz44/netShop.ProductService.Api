using System;
using System.Linq;
using FluentValidation;
using MediatR;
using netShop.Application.Features.Commands;
using netShop.Application.Wrappers;

namespace netShop.Application.Validators
{
    public class ValidatorCheck
    {
        public static object Validate<T, RT>(IValidator<T> validator, T request) where T : IBaseRequest
        {
            var validationResult = validator.Validate(request);
            if (!validationResult.IsValid)
            {
                return (RT)Activator.CreateInstance(typeof(RT), new object[] { "Validation Error", validationResult.Errors.Select(x => $"{x.ErrorCode} : {x.ErrorMessage}").ToArray() });
                //return new RT("Validation Error", validationResult.Errors.Select(x => $"{x.ErrorCode} : {x.ErrorMessage}").ToArray());
            }

            return null;
        }
    }
}