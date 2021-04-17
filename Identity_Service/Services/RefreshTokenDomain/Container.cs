using Microsoft.Extensions.DependencyInjection;
using Services.RefreshTokenDomain.Abstraction;
using Services.RefreshTokenDomain.Implementation;

namespace Services.RefreshTokenDomain
{
    public static class Container
    {
        public static IServiceCollection AddRefreshTokenServices(this IServiceCollection services)
        {
            services.AddTransient(typeof(IGenerateResreshTokenService), typeof(GenerateResreshTokenService));
            services.AddTransient(typeof(IGetRefreshTokenService), typeof(GetRefreshTokenService));
            services.AddTransient(typeof(IUpdateResreshTokenService), typeof(UpdateResreshTokenService));

            return services;
        }
    }
}
