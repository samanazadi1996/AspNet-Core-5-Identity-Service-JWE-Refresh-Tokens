using Microsoft.Extensions.DependencyInjection;
using Services.CommonServices;
using Services.JWTServices;

namespace Services
{
    public static class Continer
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddCommonService();
            services.AddJWTService();
            return services;
        }
    }
}
