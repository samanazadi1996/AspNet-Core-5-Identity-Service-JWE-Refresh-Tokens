using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Presentation.WebUI.Infrastructure.Authentication.DTO;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.WebUI.Infrastructure.Authentication.Middlewares
{

    public class ApiAuthentication : IMiddleware
    {
        private readonly ApiAuthenticationOptions options;
        private readonly AuthenticatedUser authenticatedUser;

        public ApiAuthentication(
            ApiAuthenticationOptions options,
            AuthenticatedUser authenticatedUser
            )
        {
            this.options = options;
            this.authenticatedUser = authenticatedUser;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var token = context.Session.GetString("token");
            if (token is not null)
            {
                var ppp = await Authenticate(context, token);
                if (!ppp)
                {
                    var refreshtoken = context.Session.GetString("refreshtoken");
                    var Getrefreshtoken = await Request<TokensDTO>($"{options.Domain}api/v1/Authentication/RefreshToken", "refreshtoken", refreshtoken);
                    if (Getrefreshtoken is not null)
                    {
                        context.Session.SetString("refreshtoken", Getrefreshtoken.Data.refreshToken);
                        context.Session.SetString("token", Getrefreshtoken.Data.token);
                        await Authenticate(context, Getrefreshtoken.Data.token);
                    }
                }
            }
            await next.Invoke(context);
        }

        private async Task<bool> Authenticate(HttpContext context, string token)
        {
            var result = await Request<AuthenticatedUser>($"{options.Domain}api/v1/Authentication/Authenticate", "token", token);

            if (result is not null)
            {
                authenticatedUser.IsAuthenticated = true;
                authenticatedUser.Name = result.Data.Name;
                authenticatedUser.Roles = result.Data.Roles;
                return true;
            }

            return false;

        }
        private async Task<ApiResult<T>> Request<T>(string url, string dataType, string data)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var result = await client.GetAsync($"{url}?{dataType}={data}");
                    string resultContent = await result.Content.ReadAsStringAsync();

                    if (result.IsSuccessStatusCode)
                    {
                        var apiResult = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiResult<T>>(resultContent);
                        return apiResult;
                    }

                    return null;
                }

            }
            catch (Exception)
            {
                return null;
            }
        }

    }

}
