using System;
using System.Collections.Generic;
using System.Text; 
using AutoMapper;
using NetShop.ProductService.Application.Features.Commands.SupplierCommands;
using NetShop.ProductService.Application.Features.Queries.SupplierQueries;

namespace NetShop.ProductService.Application.Mappings
{
    public class SupplierMapper: Profile
    {
        public SupplierMapper()
        {
            CreateMap<Domain.Entities.Supplier, Dtos.SupplierDto>()
                .ReverseMap();
                
            CreateMap<Domain.Entities.Supplier, CreateSupplierCommand>()
                .ReverseMap();
            CreateMap<Domain.Entities.Supplier, UpdateSupplierCommand>()
                .ReverseMap();
            CreateMap<Domain.Entities.Supplier, DeleteSupplierCommand>()
                .ReverseMap();
            CreateMap<Domain.Entities.Supplier, GetSupplierDetailQuery>()
                .ReverseMap();
            CreateMap<Domain.Entities.Supplier, FindSuppliersQuery>()
                .ReverseMap();
        }
    }
}