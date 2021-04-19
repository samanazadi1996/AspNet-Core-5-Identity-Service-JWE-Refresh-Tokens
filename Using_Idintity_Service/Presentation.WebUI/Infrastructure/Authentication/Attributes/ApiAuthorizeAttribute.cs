using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Presentation.WebUI.Infrastructure.Authentication.DTO;
using Presentation.WebUI.Infrastructure.Authentication.Services.Autorize;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.WebUI.Infrastructure.Authentication.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]

    public class ApiAuthorizeAttribute : ActionFilterAttribute
    {
        public string Role { get; set; }
        public string[] Permission { get; set; }

        public ApiAuthorizeAttribute(params string[] Permission)
        {
            this.Permission = Permission;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var options = context.HttpContext.RequestServices.GetRequiredService<ApiAuthenticationOptions>();
            var authenticatedUser = context.HttpContext.RequestServices.GetRequiredService<AuthenticatedUser>();

            if (!authenticatedUser.IsAuthenticated)
                context.Result = new RedirectResult(options.LoginPath);

            if (Role is not null && !authenticatedUser.IsInRole(Role))
                context.Result = new RedirectResult(options.LoginPath);

            if (authenticatedUser.IsAuthenticated && Permission.Any())
            {
                var autorizeService = context.HttpContext.RequestServices.GetRequiredService<IAutorizeService>();
                if (!autorizeService.AllowAccess(context.HttpContext, authenticatedUser.Name, Permission.ToList()))
                {
                    context.Result = new RedirectResult(options.LoginPath);
                }
            }
            base.OnActionExecuting(context);
        }
    }
}

