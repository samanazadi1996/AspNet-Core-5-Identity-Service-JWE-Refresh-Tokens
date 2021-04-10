using Microsoft.Extensions.DependencyInjection;
using Services.JWTDomain;
using Services.RefreshTokenDomain;

namespace Services
{
    public static class Continer
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddJWTService();
            services.AddRefreshTokenServices();
            return services;
        }
    }
}
