using Microsoft.Extensions.DependencyInjection;
using Services.JWTServices;

namespace Services
{
    public static class Continer
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddJWTService();
            return services;
        }
    }
}
