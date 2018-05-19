using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using refactor_me.DTO;
using refactor_me.Models;

namespace UnitTestProject1.TestData
{
    public static class TestMapperConfig
    {
        public static IMapper Mapper()
        {
            // Could have used Profile but i'm being lazy
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ProductDto, Product>()
                    .ForMember(dest => dest.ProductOption, opt => opt.Ignore())
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.DeliveryPrice, opt => opt.MapFrom(src => src.DeliveryPrice))
                    .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                    .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                    .ReverseMap();

                cfg.CreateMap<ProductOption, ProductOptionsDto>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                    .ForSourceMember(src => src.Product, opt => opt.Ignore())
                    .ReverseMap();
            });

            return config.CreateMapper();
        }
    }
}
