using Microsoft.Extensions.DependencyInjection;
using Presentation.WebUI.Infrastructure.Authentication.DTO;
using Presentation.WebUI.Infrastructure.Authentication.Middlewares;
using System;

namespace Presentation.WebUI.Infrastructure.Authentication.Services
{
    public static class ApiAuthenticationServicesExtensions
    {
        public static void AddApiAuthentication(this IServiceCollection services, Action<ApiAuthenticationOptions> configureOptions)
        {
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromDays(1);
            });

            ApiAuthenticationOptions options = new();
            configureOptions(options);
            services.AddSingleton(options);

            services.AddScoped<AuthenticatedUser>();
            services.AddScoped<ApiAuthentication>();
        }
    }
}
