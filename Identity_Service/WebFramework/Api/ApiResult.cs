using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace WebFramework.Api
{
    public class ApiResult : IActionResult
    {
        public ApiResult(object data, string message, int? status)
        {
            Message = message ?? ((HttpStatusCode)status).ToString();
            Status = status;
            Data = data;
        }
        protected int? Status { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public async Task ExecuteResultAsync(ActionContext context)
        {
            var objectResult = new ObjectResult(new ApiResult(Data, Message, Status))
            {
                StatusCode = Status ?? context.HttpContext.Response.StatusCode
            };

            await objectResult.ExecuteResultAsync(context);
        }
        #region Implicit Operators
        public static implicit operator ApiResult(OkResult result)
        {
            return new ApiResult(null, null, result.StatusCode);
        }

        public static implicit operator ApiResult(BadRequestResult result)
        {
            return new ApiResult(null, null, result.StatusCode);
        }

        public static implicit operator ApiResult(BadRequestObjectResult result)
        {
            var message = result.Value?.ToString();
            if (result.Value is SerializableError errors)
            {
                var errorMessages = errors.SelectMany(p => (string[])p.Value).Distinct();
                message = string.Join(" | ", errorMessages);
            }
            return new ApiResult(null, message, result.StatusCode);
        }

        public static implicit operator ApiResult(ContentResult result)
        {
            return new ApiResult(null, null, result.StatusCode);
        }

        public static implicit operator ApiResult(NotFoundResult result)
        {
            return new ApiResult(null, null, result.StatusCode);
        }
        #endregion

    }
    public class ApiResult<TData> : ApiResult where TData : class
    {
        public ApiResult(TData data, string message = null, int? status = null) : base(data, message, status)
        {
            Data = data;
        }

        public static implicit operator ApiResult<TData>(TData data)
        {
            return new ApiResult<TData>(data, null, (int)HttpStatusCode.OK);
        }

        public static implicit operator ApiResult<TData>(OkResult result)
        {
            return new ApiResult<TData>(null, null, result.StatusCode);
        }

        public static implicit operator ApiResult<TData>(OkObjectResult result)
        {
            return new ApiResult<TData>((TData)result.Value, null, result.StatusCode);
        }

        public static implicit operator ApiResult<TData>(BadRequestResult result)
        {
            return new ApiResult<TData>(null, null, result.StatusCode);
        }

        public static implicit operator ApiResult<TData>(BadRequestObjectResult result)
        {
            var message = result.Value?.ToString();
            if (result.Value is SerializableError errors)
            {
                var errorMessages = errors.SelectMany(p => (string[])p.Value).Distinct();
                message = string.Join(" | ", errorMessages);
            }
            return new ApiResult<TData>(null, message, result.StatusCode);
        }

        public static implicit operator ApiResult<TData>(NotFoundResult result)
        {
            return new ApiResult<TData>(null, null, result.StatusCode);
        }

        public static implicit operator ApiResult<TData>(NotFoundObjectResult result)
        {
            return new ApiResult<TData>((TData)result.Value, null, result.StatusCode);
        }
        public static implicit operator ApiResult<TData>(CreatedAtActionResult result)
        {
            return new ApiResult<TData>((TData)result.Value, null, result.StatusCode);
        }

    }
}