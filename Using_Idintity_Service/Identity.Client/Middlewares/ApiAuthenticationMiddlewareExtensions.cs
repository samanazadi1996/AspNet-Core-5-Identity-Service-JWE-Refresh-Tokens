using Microsoft.AspNetCore.Builder;
using System;

namespace Identity.Client.Middlewares
{
    public static class ApiAuthenticationMiddlewareExtensions
    {
        public static IApplicationBuilder UseApiAuthentication(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ApiAuthentication>();
        }
    }
}
