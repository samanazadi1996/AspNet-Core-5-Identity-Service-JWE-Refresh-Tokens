using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Presentation.WebUI.Infrastructure.Authentication.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.WebUI.Infrastructure.Authentication.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]

    public class ApiAuthorizeAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var options = context.HttpContext.RequestServices.GetRequiredService<ApiAuthenticationOptions>();
            var authenticatedUser = context.HttpContext.RequestServices.GetRequiredService<AuthenticatedUser>();
            if (!authenticatedUser.IsAuthenticated)
            {
                context.Result = new RedirectResult(options.LoginPath);
            }

            base.OnActionExecuting(context);
        }
    }
}
