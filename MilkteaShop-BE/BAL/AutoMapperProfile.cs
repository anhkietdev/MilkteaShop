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

            CreateMap<Order, OrderRequestDto>().ReverseMap();
            CreateMap<Order, OrderResponseDto>()
                .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems))
                .ForMember(dest => dest.StoreId, opt => opt.MapFrom(src => src.StoreId))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.PaymentMethod, opt => opt.MapFrom(src => src.PaymentMethod))
                .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.OrderNumber, opt => opt.MapFrom(src => src.OrderNumber))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Store, opt => opt.MapFrom(src => src.Store))                
                .ReverseMap();

            CreateMap<ProductSize, ProductSizeResponseDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.ProductName))
                .ForMember(dest => dest.Size, opt => opt.MapFrom(src => src.Size))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price));
            CreateMap<ProductSize, ProductSizeRequestDto>().ReverseMap();


            CreateMap<OrderItem, OrderItemRequestDto>()
                .ForMember(dest => dest.ProductSizeId, opt => opt.MapFrom(src => src.ProductSizeId))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.ToppingItems, opt => opt.MapFrom(src => src.ToppingItems))
                .ForMember(dest => dest.ParentOrderItemId, opt => opt.MapFrom(src => src.ParentOrderItemId))
                .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.OrderId))                
                .ReverseMap();
            CreateMap<OrderItem, OrderItemResponseDto>()
                .ForMember(dest => dest.ProductSize, opt => opt.MapFrom(src => src.ProductSize))
                .ForMember(dest => dest.ToppingItems, opt => opt.MapFrom(src => src.ToppingItems))
                .ForMember(dest => dest.ParentOrderItem, opt => opt.MapFrom(src => src.ParentOrderItem))
                .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.OrderId))
                .ForMember(dest => dest.ProductSizeId, opt => opt.MapFrom(src => src.ProductSizeId))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.ParentOrderItemId, opt => opt.MapFrom(src => src.ParentOrderItemId))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ReverseMap();

        }
    }
}
