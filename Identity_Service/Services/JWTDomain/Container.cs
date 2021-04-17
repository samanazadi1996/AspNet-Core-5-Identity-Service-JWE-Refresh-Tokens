using Microsoft.Extensions.DependencyInjection;
using Services.JWTDomain.Abstraction;
using Services.JWTDomain.Implementation;

namespace Services.JWTDomain
{
    public static class Container
    {
        public static IServiceCollection AddJWTService(this IServiceCollection services)
        {
            services.AddTransient(typeof(IJwtService), typeof(JwtService));
            services.AddTransient(typeof(IGetClaimsByTokenService), typeof(GetClaimsByTokenService));

            return services;
        }
    }
}
