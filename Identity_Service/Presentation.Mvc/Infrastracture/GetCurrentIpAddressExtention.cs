using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;


namespace Presentation.Mvc.Infrastracture
{
    public static class GetCurrentIpAddressExtention
    {
        public static string Get(HttpContext context)
        {
            string IpAddress;
            if (!string.IsNullOrEmpty(context.Request.Headers["X-Forwarded-For"]))
                IpAddress = context.Request.Headers["X-Forwarded-For"];
            else
                IpAddress = context.Request.HttpContext.Features.Get<IHttpConnectionFeature>().RemoteIpAddress.ToString();
            return IpAddress;
        }
    }
}
