using Data.Repositores.Abstraction;
using Microsoft.Extensions.DependencyInjection;

namespace Data.Repositores
{
    public static class Container
    {
        public static IServiceCollection AddRepositoryServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRefreshTokenRepository), typeof(RefreshTokenRepository));

            return services;
        }
    }
}
