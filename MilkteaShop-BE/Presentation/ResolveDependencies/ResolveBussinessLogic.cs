using BAL;
using BAL.Services.Implement;
using BAL.Services.Interface;
using DAL.Context;
using DAL.Repositories.Implements;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Presentation.ResolveDependencies
{
    public static class ResolveBussinessLogic
    {
        public static IServiceCollection ResolveServices(this IServiceCollection services, string connectionString)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IComboItemRepository, ComboItemRepository>();
            services.AddScoped<ICategoryExtraMappingRepository, CategoryExtraMappingRepository>();
            services.AddScoped<IOrderItemRepository, OrderItemRepository>();
            services.AddScoped<IPaymentMethodRepository, PaymentMethodRepository>();

            services.AddDbContext<AppDbContext>(option => option.UseSqlServer(connectionString));


            return services;
        }
    }
}
