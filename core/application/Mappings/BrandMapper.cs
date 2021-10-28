using System;
using System.Collections.Generic;
using System.Text; 
using AutoMapper;
using NetShop.ProductService.Application.Features.Commands.BrandCommands;
using NetShop.ProductService.Application.Features.Queries.BrandQueries;

namespace NetShop.ProductService.Application.Mappings
{
    public class BrandMapper: Profile
    {
        public BrandMapper()
        {
            CreateMap<Domain.Entities.Brand, Dtos.BrandDto>()
                .ReverseMap();
                
            CreateMap<Domain.Entities.Brand, CreateBrandCommand>()
                .ReverseMap();
            CreateMap<Domain.Entities.Brand, UpdateBrandCommand>()
                .ReverseMap();
            CreateMap<Domain.Entities.Brand, DeleteBrandCommand>()
                .ReverseMap();
            CreateMap<Domain.Entities.Brand, GetBrandDetailQuery>()
                .ReverseMap();
            CreateMap<Domain.Entities.Brand, FindBrandsQuery>()
                .ReverseMap();
        }
    }
}