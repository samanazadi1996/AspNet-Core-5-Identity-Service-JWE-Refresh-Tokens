using Identity.Client.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Client.Services.ApiRequest
{
    public interface IApiRequestService
    {
        Task<ApiResult<T>> RequestGet<T>(string url);

        Task<ApiResult<T>> RequestDelete<T>(string url);

        Task<ApiResult<T>> RequestPost<T>(string url, object model);

        Task<ApiResult> RequestDelete(string url);

        Task<ApiResult> RequestPost(string url, object model);

        Task<ApiResult> RequestPut(string url, object model);
    }

}
