using System;
using System.Collections.Generic;
using System.Text; 
using System.Reflection; 
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using FluentValidation.AspNetCore;
using FluentValidation;

namespace netShop.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationRegistration(this IServiceCollection services)
        {
            var assmb = Assembly.GetExecutingAssembly();
            
            services.AddAutoMapper(assmb);
            services.AddMediatR(assmb);
            // services.AddFluentValidation(fv =>
            // {
            //     fv.ImplicitlyValidateChildProperties = true;
            //     fv.ImplicitlyValidateRootCollectionElements = true;                
            //     fv.RegisterValidatorsFromAssembly(assmb);
            // });

            var assembliesToRegister = new List<Assembly>() { assmb };
            AssemblyScanner.FindValidatorsInAssemblies(assembliesToRegister).ForEach(pair => {
                services.Add(ServiceDescriptor.Transient(pair.InterfaceType, pair.ValidatorType));
            });
        }
    }
}