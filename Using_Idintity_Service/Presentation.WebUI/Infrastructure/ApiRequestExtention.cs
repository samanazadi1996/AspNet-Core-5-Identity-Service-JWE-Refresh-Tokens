using Microsoft.AspNetCore.Http;
using Presentation.WebUI.Infrastructure.Authentication.DTO;
using System.Net.Http;
using System.Text;

namespace Presentation.WebUI.Infrastructure
{
    public static class ApiRequestExtention
    {
        const string identityDomain = "https://localhost:44390/";
        public static ApiResult<T> RequestPost<T>(HttpContext context, string url, object model)
        {
            try
            {
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(model);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                using var client = new HttpClient();
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {context.Session.GetString("token")}");
                var response = client.PostAsync($"{identityDomain}{url}", data).Result;
                string result = response.Content.ReadAsStringAsync().Result;
                return Newtonsoft.Json.JsonConvert.DeserializeObject<ApiResult<T>>(result);
            }
            catch (System.Exception)
            {
                return null;
            }
        }
        public static ApiResult<T> RequestGet<T>(HttpContext context, string url)
        {
            try
            {
                using var client = new HttpClient();
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {context.Session.GetString("token")}");
                var result = client.GetAsync($"{identityDomain}{url}").Result;
                string json = result.Content.ReadAsStringAsync().Result;
                var model = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiResult<T>>(json);
                return model;

            }
            catch (System.Exception)
            {
                return null;
            }
        }
    }
}
