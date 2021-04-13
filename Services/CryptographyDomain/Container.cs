using Microsoft.Extensions.DependencyInjection;
using Services.CryptographyDomain.Abstraction;
using Services.CryptographyDomain.Implementation;

namespace Services.CryptographyDomain
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
