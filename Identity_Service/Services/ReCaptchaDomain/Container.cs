using Microsoft.Extensions.DependencyInjection;
using Services.ReCaptchaDomain.Abstraction;
using Services.ReCaptchaDomain.Implementation;

namespace Services.ReCaptchaDomain
{
    public static class Container
    {
        public static IServiceCollection AddReCaptchaService(this IServiceCollection services)
        {
            services.AddTransient(typeof(IGetImageReCaptchaService), typeof(GetImageReCaptchaService));

            return services;
        }
    }
}
