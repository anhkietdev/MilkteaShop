using AutoMapper;
using BAL;
using BAL.Services.Implement;
using BAL.Services.Interface;
using DAL.Models;
using DAL.Repositories.Implements;
using DAL.Repositories.Interfaces;

namespace Presentation.ResolveDependencies
{
    public static class ResolveBussinessLogic
    {
        public static IServiceCollection ResolveServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IProductService, ProductService>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IComboItemRepository, ComboItemRepository>();
            services.AddScoped<ICategoryExtraMappingRepository, CategoryExtraMappingRepository>();
            services.AddScoped<IOrderItemRepository, OrderItemRepository>();
            services.AddScoped<IPaymentMethodRepository, PaymentMethodRepository>();

            return services;
        }
    }
}
