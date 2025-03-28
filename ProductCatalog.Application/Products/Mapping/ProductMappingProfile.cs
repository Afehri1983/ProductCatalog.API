using AutoMapper;
using ProductCatalog.Application.Products.DTOs;
using ProductCatalog.Domain.Entities;

namespace ProductCatalog.Application.Products.Mapping;

public class ProductMappingProfile : Profile
{
    public ProductMappingProfile()
    {
        CreateMap<Product, ProductDto>();
        CreateMap<ProductDto, Product>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => 0)); // New products start with Id 0
    }
} 