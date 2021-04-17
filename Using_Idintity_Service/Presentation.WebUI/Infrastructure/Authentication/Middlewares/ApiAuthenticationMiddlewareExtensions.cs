using Microsoft.AspNetCore.Builder;

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
