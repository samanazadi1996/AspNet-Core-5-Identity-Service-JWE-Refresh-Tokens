using Identity.Client;
using Identity.Client.DTO;
using Identity.Client.Middlewares;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Presentation.WebUI.Infrastructure.Authentication.Services
{
    public static class ApiAuthenticationServicesExtensions
    {
        public static void AddApiAuthentication(this IServiceCollection services, Action<ApiAuthenticationOptions> configureOptions)
        {
            ApiAuthenticationOptions options = new();
            configureOptions(options);
            services.AddSingleton(options);

            services.AddScoped<AuthenticatedUser>();
            services.AddScoped<ApiAuthentication>();
        }
    }
}
