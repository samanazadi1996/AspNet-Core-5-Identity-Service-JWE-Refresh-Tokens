using Microsoft.Extensions.DependencyInjection;
using Services.JWTServices.Abstraction;
using Services.JWTServices.Implementation;

namespace Services.JWTServices
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
