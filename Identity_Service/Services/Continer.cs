using Microsoft.Extensions.DependencyInjection;
using Services.CryptographyDomain;
using Services.JWTDomain;
using Services.MailDomain;
using Services.ReCaptchaDomain;
using Services.RefreshTokenDomain;

namespace Services
{
    public static class Continer
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddJWTService();
            services.AddRefreshTokenServices();
            services.AddTextEncryptionServices();
            services.AddMailService();
            services.AddReCaptchaService();

            return services;
        }
    }
}
