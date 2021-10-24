using System;
using System.Collections.Generic;
using System.Text; 
using AutoMapper;
using netShop.Application.Features.Commands.BrandCommands;
using netShop.Application.Features.Queries.BrandQueries;

namespace netShop.Application.Mappings
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