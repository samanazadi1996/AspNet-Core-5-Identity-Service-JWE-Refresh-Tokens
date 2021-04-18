using Microsoft.AspNetCore.Builder;
using System;

namespace Presentation.WebUI.Infrastructure.Authentication.Middlewares
{
    public static class ApiAuthenticationMiddlewareExtensions
    {
        public static IApplicationBuilder UseApiAuthentication(this IApplicationBuilder builder)
        {
            builder.UseSession();

            return builder.UseMiddleware<ApiAuthentication>();
        }
    }
}
