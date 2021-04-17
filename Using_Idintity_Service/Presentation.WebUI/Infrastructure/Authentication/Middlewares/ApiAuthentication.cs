using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Presentation.WebUI.Infrastructure.Authentication.DTO;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.WebUI.Infrastructure.Authentication.Middlewares
{

    public class ApiAuthentication
    {
        private readonly RequestDelegate _next;

        [Obsolete]
        public ApiAuthentication(RequestDelegate next)
        {
            _next = next;
        }

        [Obsolete]
        public async Task Invoke(HttpContext context)
        {
            var token = context.Session.GetString("token");
            if (token is not null)
            {
                var ppp = await Authenticate(context, token);
                if (!ppp)
                {
                    var refreshtoken = context.Session.GetString("refreshtoken");
                    var Getrefreshtoken = await Request<TokensDTO>("https://localhost:44390/api/v1/Authentication/RefreshToken", "refreshtoken", refreshtoken);
                    if (Getrefreshtoken is not null)
                    {
                        context.Session.SetString("refreshtoken", Getrefreshtoken.Data.refreshToken);
                        context.Session.SetString("token", Getrefreshtoken.Data.token);
                        await Authenticate(context, Getrefreshtoken.Data.token);
                    }
                }
            }
            await _next.Invoke(context);
        }

        private async Task<bool> Authenticate(HttpContext context, string token)
        {
            var result = await Request<AuthenticatedUser>("https://localhost:44390/api/v1/Authentication/Authenticate", "token", token);

            if (result is not null)
            {
                var authenticatedUser = context.RequestServices.GetRequiredService<AuthenticatedUser>();
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
