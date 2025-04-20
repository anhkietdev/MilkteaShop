using BAL.Services.Implement;
using BAL.Services.Interface;

namespace Presentation.ResolveDependencies
{
    public static class ResolveBussinessLogic
    {
        public static IServiceCollection ResolveServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            return services;
        }
    }
}
