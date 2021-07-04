using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using WebFramework.Api;

namespace WebFramework.Filters
{
    public class ApiResultFilterAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.Result is OkObjectResult okObjectResult)
            {
                var apiResult = new ApiResult<object>(okObjectResult.Value, null, okObjectResult.StatusCode);
                context.Result = new JsonResult(apiResult);
                context.HttpContext.Response.StatusCode = okObjectResult.StatusCode.Value;
            }
            else if (context.Result is OkResult okResult)
            {
                var apiResult = new ApiResult(null, null, okResult.StatusCode);
                context.Result = new JsonResult(apiResult);
                context.HttpContext.Response.StatusCode = okResult.StatusCode;
            }
            else if (context.Result is BadRequestResult badRequestResult)
            {
                var apiResult = new ApiResult(null, null, badRequestResult.StatusCode);
                context.Result = new JsonResult(apiResult);
                context.HttpContext.Response.StatusCode = badRequestResult.StatusCode;

            }
            else if (context.Result is BadRequestObjectResult badRequestObjectResult)
            {
                var messages = ((Microsoft.AspNetCore.Mvc.ValidationProblemDetails)badRequestObjectResult.Value).Errors;
                var message = "";
                if (badRequestObjectResult.Value is SerializableError errors)
                {
                    var errorMessages = errors.SelectMany(p => (string[])p.Value).Distinct();
                    message = string.Join(" | ", errorMessages);
                }
                else
                {
                    foreach (var item in messages)
                    {
                        foreach (var vals in item.Value)
                        {
                            if (string.IsNullOrEmpty(message))
                                message = vals.ToString();
                            else
                                message = string.Join(" | ", vals.ToString(), message);
                        }
                    }
                }
                var apiResult = new ApiResult(null, message, badRequestObjectResult.StatusCode);
                context.Result = new JsonResult(apiResult);
                context.HttpContext.Response.StatusCode = badRequestObjectResult.StatusCode.Value;

            }
            else if (context.Result is ContentResult contentResult)
            {
                var apiResult = new ApiResult(null, contentResult.Content, contentResult.StatusCode);
                context.Result = new JsonResult(apiResult);
                context.HttpContext.Response.StatusCode = contentResult.StatusCode.Value;
            }
            else if (context.Result is NotFoundResult notFoundResult)
            {
                var apiResult = new ApiResult(null, null, notFoundResult.StatusCode);
                context.Result = new JsonResult(apiResult);
                context.HttpContext.Response.StatusCode = notFoundResult.StatusCode;
            }
            else if (context.Result is NotFoundObjectResult notFoundObjectResult)
            {
                var apiResult = new ApiResult<object>(notFoundObjectResult.Value, null, notFoundObjectResult.StatusCode);
                context.Result = new JsonResult(apiResult);
                context.HttpContext.Response.StatusCode = notFoundObjectResult.StatusCode.Value;
            }
            else if (context.Result is ObjectResult objectResult && objectResult.StatusCode == null
                && !(objectResult.Value is ApiResult))
            {
                var apiResult = new ApiResult<object>(objectResult.Value, null, objectResult.StatusCode);
                context.Result = new JsonResult(apiResult);
                context.HttpContext.Response.StatusCode = objectResult.StatusCode.Value;
            }

            base.OnResultExecuting(context);
        }
    }
}
