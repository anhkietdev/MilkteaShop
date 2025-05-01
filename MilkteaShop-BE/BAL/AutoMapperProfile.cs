using AutoMapper;
using BAL.Dtos;
using DAL.Models;

namespace BAL
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Create mappings between DTOs and entities here

            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<ComboItem, ComboItemDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Store, StoreDto>().ReverseMap();
            CreateMap<Order, OrderResponseDto>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
                .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems))
                .ReverseMap();
            CreateMap<Order, OrderRequestDto>().ReverseMap();
            CreateMap<Order, OrderResponseDto>()
                    .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
                    .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems))
                    .ReverseMap();
            CreateMap<ProductSize, ProductSizeResponseDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.ProductName))
                .ForMember(dest => dest.Size, opt => opt.MapFrom(src => src.Size))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price));
            CreateMap<ProductSize, ProductSizeRequestDto>().ReverseMap();

        }
    }
}
