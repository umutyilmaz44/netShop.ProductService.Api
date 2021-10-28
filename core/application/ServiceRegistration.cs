using System;
using System.Collections.Generic;
using System.Text; 
using System.Reflection; 
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using FluentValidation.AspNetCore;
using FluentValidation;
using NetShop.ProductService.Application.Behaviours;

namespace NetShop.ProductService.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationRegistration(this IServiceCollection services)
        {
            var assmb = Assembly.GetExecutingAssembly();
            
            services.AddAutoMapper(assmb);
            services.AddMediatR(assmb);
            
            var assembliesToRegister = new List<Assembly>() { assmb };
            AssemblyScanner.FindValidatorsInAssemblies(assembliesToRegister).ForEach(pair => {
                services.Add(ServiceDescriptor.Transient(pair.InterfaceType, pair.ValidatorType));
            });

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

        }
    }
}