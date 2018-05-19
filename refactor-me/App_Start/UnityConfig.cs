using System.Web.Http;
using AutoMapper;
using refactor_me.Services;
using Unity;
using Unity.Lifetime;
using Unity.WebApi;
using refactor_me.Models;
using refactor_me.DTO;

namespace refactor_me
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            container.RegisterType<IProductService, ProductsService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IProductOptionsService, ProductOptionsService>(
                new ContainerControlledLifetimeManager());

            // since we have a pretty small app,
            // i'm just going to leave this configuration here
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

            IMapper mapper = config.CreateMapper();

            container.RegisterInstance(mapper);
            
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}