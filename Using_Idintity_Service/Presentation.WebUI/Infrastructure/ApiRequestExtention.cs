using Microsoft.AspNetCore.Http;
using Presentation.WebUI.Infrastructure.Authentication.DTO;
using System.Net.Http;
using System.Text;

namespace Presentation.WebUI.Infrastructure
{
    public static class ApiRequestExtention
    {
        public static string RequestPost<T>(HttpContext context, string url, T model)
        {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(model);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {context.Session.GetString("token")}");
            var response = client.PostAsync(url, data).Result;
            string result = response.Content.ReadAsStringAsync().Result;
            return result;
        }
        public static ApiResult<T> RequestGet<T>(HttpContext context, string url)
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {context.Session.GetString("token")}");
            var result = client.GetAsync(url).Result;
            string json = result.Content.ReadAsStringAsync().Result;
            var model = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiResult<T>>(json);
            return model;
        }
    }
}
