using Microsoft.Extensions.DependencyInjection;
using Services.RefreshTokenDomain.Abstraction;
using Services.RefreshTokenDomain.Implementation;
using Services.TextEncryptionDomain.Abstraction;
using Services.TextEncryptionDomain.Implementation;

namespace Services.TextEncryptionDomain
{
    public static class Container
    {
        public static IServiceCollection AddTextEncryptionServices(this IServiceCollection services)
        {
            services.AddTransient(typeof(IDecryptService), typeof(DecryptService));
            services.AddTransient(typeof(IEncryptService), typeof(EncryptService));

            return services;
        }
    }
}
