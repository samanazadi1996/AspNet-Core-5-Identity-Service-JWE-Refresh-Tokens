using Microsoft.Extensions.DependencyInjection;
using System;

namespace Presentation.WebUI.Models.Authentication.Services
{
    public static class ApiAuthenticationServicesExtensions
    {
        public static void AddApiAuthentication(this IServiceCollection services)
        {
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromDays(1);
            });

            services.AddScoped<AuthenticatedUser>();
        }
    }

}
