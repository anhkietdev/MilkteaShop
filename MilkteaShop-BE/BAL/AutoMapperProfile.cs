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

            CreateMap<Voucher, VoucherRequestDto>().ReverseMap();

            CreateMap<Voucher, VoucherResponseDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.VoucherCode, opt => opt.MapFrom(src => src.VoucherCode))
                .ForMember(dest => dest.DiscountPercentage, opt => opt.MapFrom(src => src.DiscountPercentage))
                .ForMember(dest => dest.PriceCondition, opt => opt.MapFrom(src => src.PriceCondition))
                .ForMember(dest => dest.ExceedDate, opt => opt.MapFrom(src => src.ExceedDate))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt));
            // User mappings with improved handling for registration flow
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.ToString()))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt));

            // NewRegisterDto to User mapping - critical for registration
            CreateMap<NewRegisterDto, User>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role))
                .ForMember(dest => dest.StoreId, opt => opt.MapFrom(src => src.StoreId))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true));

            // User to NewRegisterDto mapping (less common use case)
            CreateMap<User, NewRegisterDto>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.PasswordHash))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role))
                .ForMember(dest => dest.StoreId, opt => opt.MapFrom(src => src.StoreId));

            // User to AuthenResultDto mapping for authentication response
            CreateMap<User, AuthenResultDto>()
                .ForMember(dest => dest.IsSuccess, opt => opt.MapFrom(src => true))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.ToString()))
                .ForMember(dest => dest.Token, opt => opt.Ignore())
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id));



            CreateMap<Store, StoreDto>().ReverseMap();
            CreateMap<Store, StoreResponeDto>()
       .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
       .ForMember(dest => dest.StoreName, opt => opt.MapFrom(src => src.StoreName))
       .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
       .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
       .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
       .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive))
       .ForMember(dest => dest.Orders, opt => opt.MapFrom(src => src.Orders))  // Assuming Orders is a collection
       .ForMember(dest => dest.Users, opt => opt.MapFrom(src => src.Users))  // Assuming Users is a collection
       .ForMember(dest => dest.UserIds, opt => opt.MapFrom(src => src.Users.Select(u => u.Id).ToList())) // Example for mapping UserIds from a collection of UserDto
       .ForMember(dest => dest.OrderIds, opt => opt.MapFrom(src => src.Orders.Select(o => o.Id).ToList()))  // Example for mapping OrderIds from a collection of OrderDto
         .ForMember(dest => dest.CashBalance, opt => opt.MapFrom(src => src.CashBalance))
       .ReverseMap();


            CreateMap<CategoryExtraMapping, CategoryExtraMappingDto>().ReverseMap();


            CreateMap<Order, OrderRequestDto>().ReverseMap();
            CreateMap<Order, OrderComboRequest>().ReverseMap();

            CreateMap<Order, OrderStoreResponseDto>()
                .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems))

                .ForMember(dest => dest.PaymentMethod, opt => opt.MapFrom(src => src.PaymentMethod))
                .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount))

                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.OrderNumber, opt => opt.MapFrom(src => src.OrderNumber))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))

                .ReverseMap();
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
                .ForMember(dest => dest.OrderStatus, opt => opt.MapFrom(src => src.OrderStatus))
                .ReverseMap();

            CreateMap<ProductSize, ProductSizeResponseDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.ProductName))
                .ForMember(dest => dest.Size, opt => opt.MapFrom(src => src.Size))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price));
            CreateMap<ProductSize, ProductSizeRequestDto>().ReverseMap();


            CreateMap<OrderItemTopping, OrderItemToppingDto>()
                .ForMember(dest => dest.ToppingProductSizeId, opt => opt.MapFrom(src => src.ProductSizeId))
                .ForMember(dest => dest.ToppingProductSize, opt => opt.MapFrom(src => src.ProductSize));

            CreateMap<OrderItem, OrderItemResponseDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.OrderId))
                .ForMember(dest => dest.ProductSizeId, opt => opt.MapFrom(src => src.ProductSizeId))
                .ForMember(dest => dest.ProductSize, opt => opt.MapFrom(src => src.ProductSize))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Toppings, opt => opt.MapFrom(src => src.Toppings));
            CreateMap<OrderItem, OrderItemRequestDto>().ReverseMap();

            CreateMap<Store, OrderSummaryDto>()
                .ForMember(dest => dest.StoreId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.StoreName, opt => opt.MapFrom(src => src.StoreName));
        }
    }
}
