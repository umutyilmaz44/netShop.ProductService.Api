using System;
using System.Collections.Generic;
using System.Text; 
using AutoMapper;
using netShop.Application.Features.Commands.BrandModelCommands;
using netShop.Application.Features.Queries.BrandModelQueries;

namespace netShop.Application.Mappings
{
    public class BrandModelMapper: Profile
    {
        public BrandModelMapper()
        {
            CreateMap<Domain.Entities.BrandModel, Dtos.BrandModelDto>()
                .ReverseMap();

            CreateMap<Domain.Entities.BrandModel, CreateBrandModelCommand>()
                .ReverseMap();
            CreateMap<Domain.Entities.BrandModel, UpdateBrandModelCommand>()
                .ReverseMap();
            CreateMap<Domain.Entities.BrandModel, DeleteBrandModelCommand>()
                .ReverseMap();
            CreateMap<Domain.Entities.BrandModel, GetBrandModelDetailQuery>()
                .ReverseMap();
            CreateMap<Domain.Entities.BrandModel, FindBrandModelsQuery>()
                .ReverseMap();
        }
    }
}