using System;
using System.Collections.Generic;
using System.Text; 
using AutoMapper;
using netShop.Application.Features.Commands.ProductCommands;
using netShop.Application.Features.Queries.ProductQueries;

namespace netShop.Application.Mappings
{
    public class ProductMapper: Profile
    {
        public ProductMapper()
        {
            CreateMap<Domain.Entities.Product, Dtos.ProductDto>()
                .ReverseMap();
            CreateMap<Domain.Entities.Product, CreateProductCommand>()
                .ReverseMap();
            CreateMap<Domain.Entities.Product, UpdateProductCommand>()
                .ReverseMap();
            CreateMap<Domain.Entities.Product, DeleteProductCommand>()
                .ReverseMap();
            CreateMap<Domain.Entities.Product, GetProductDetailQuery>()
                .ReverseMap();
            CreateMap<Domain.Entities.Product, FindProductsQuery>()
                .ReverseMap();
        }
    }
}