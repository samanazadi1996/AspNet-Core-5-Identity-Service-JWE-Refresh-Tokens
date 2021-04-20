using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Identity.Client.DTO;
using System;
using System.Linq;

namespace Identity.Client.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]

    public class ApiAuthorizeAttribute : ActionFilterAttribute
    {
        public string Role { get; set; }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var options = context.HttpContext.RequestServices.GetRequiredService<ApiAuthenticationOptions>();
            var authenticatedUser = context.HttpContext.RequestServices.GetRequiredService<AuthenticatedUser>();

            if (!authenticatedUser.IsAuthenticated)
                context.Result = new RedirectResult(options.LoginPath);

            if (Role is not null && !authenticatedUser.IsInRole(Role))
                context.Result = new RedirectResult(options.LoginPath);

            base.OnActionExecuting(context);
        }
    }
}

