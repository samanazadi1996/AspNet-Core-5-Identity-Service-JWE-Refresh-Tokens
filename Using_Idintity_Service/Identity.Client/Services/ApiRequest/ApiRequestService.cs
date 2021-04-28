using Identity.Client.DTO;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Identity.Client.Services.ApiRequest
{
    public class ApiRequestService : IApiRequestService
    {
        private readonly ApiAuthenticationOptions apiAuthenticationOptions;
        private readonly AuthenticatedUser authenticatedUser;

        public ApiRequestService(
            ApiAuthenticationOptions apiAuthenticationOptions,
            AuthenticatedUser authenticatedUser)
        {
            this.apiAuthenticationOptions = apiAuthenticationOptions;
            this.authenticatedUser = authenticatedUser;
        }

        public async Task<ApiResult<T>> RequestPost<T>(string url, object model)
        {
            try
            {
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(model);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {authenticatedUser.Token}");
                var response = await client.PostAsync($"{apiAuthenticationOptions.Domain}{url}", data);
                string result = await response.Content.ReadAsStringAsync();
                client.Dispose();

                return Newtonsoft.Json.JsonConvert.DeserializeObject<ApiResult<T>>(result);
            }
            catch (System.Exception ex)
            {
                return new ApiResult<T>() { IsSuccess = false, Message = ex.ToString(), StatusCode = -1 };
            }
        }
        public async Task<ApiResult<T>> RequestGet<T>(string url)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {authenticatedUser.Token}");
                var result = await client.GetAsync($"{apiAuthenticationOptions.Domain}{url}");
                string json = await result.Content.ReadAsStringAsync();
                client.Dispose();

                return JsonSerializer.Deserialize<ApiResult<T>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            catch (System.Exception ex)
            {
                return new ApiResult<T>() { IsSuccess = false, Message = ex.ToString(), StatusCode = -1 };
            }
        }
        public async Task<ApiResult<T>> RequestDelete<T>(string url)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {authenticatedUser.Token}");
                var result = await client.DeleteAsync($"{apiAuthenticationOptions.Domain}{url}");
                string json = await result.Content.ReadAsStringAsync();
                client.Dispose();

                return Newtonsoft.Json.JsonConvert.DeserializeObject<ApiResult<T>>(json);
            }
            catch (System.Exception ex)
            {
                return new ApiResult<T>() { IsSuccess = false, Message = ex.ToString(), StatusCode = -1 };
            }
        }

        public async Task<ApiResult> RequestDelete(string url)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {authenticatedUser.Token}");
                var result = await client.DeleteAsync($"{apiAuthenticationOptions.Domain}{url}");
                string json = await result.Content.ReadAsStringAsync();
                client.Dispose();

                return JsonSerializer.Deserialize<ApiResult>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            catch (System.Exception ex)
            {
                return new ApiResult() { IsSuccess = false, Message = ex.ToString(), StatusCode = -1 };
            }
        }

        public async Task<ApiResult> RequestPost(string url, object model)
        {
            try
            {
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(model);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {authenticatedUser.Token}");
                var response = await client.PostAsync($"{apiAuthenticationOptions.Domain}{url}", data);
                string result = await response.Content.ReadAsStringAsync();
                client.Dispose();

                return Newtonsoft.Json.JsonConvert.DeserializeObject<ApiResult>(result);
            }
            catch (System.Exception ex)
            {
                return new ApiResult() { IsSuccess = false, Message = ex.ToString(), StatusCode = -1 };
            }
        }

        public async Task<ApiResult> RequestPut(string url, object model)
        {
            try
            {
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(model);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                HttpClient client = new HttpClient();
                //client.Timeout = TimeSpan.FromSeconds(2);
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {authenticatedUser.Token}");
                var response = await client.PutAsync($"{apiAuthenticationOptions.Domain}{url}", data);
                string result = await response.Content.ReadAsStringAsync();
                client.Dispose();

                return Newtonsoft.Json.JsonConvert.DeserializeObject<ApiResult>(result);
            }
            catch (System.Exception ex)
            {
                return new ApiResult() { IsSuccess = false, Message = ex.ToString(), StatusCode = -1 };
            }
        }
    }

}
