using Microsoft.AspNetCore.Mvc;
using Presentation.WebUI.Infrastructure;
using Presentation.WebUI.Infrastructure.Authentication;
using Presentation.WebUI.Infrastructure.Authentication.Attributes;
using Presentation.WebUI.Models;
using System.Collections.Generic;

namespace Presentation.WebUI.Controllers
{
    [ApiAuthorize]
    public class UserRolesController : Controller
    {
        private readonly AuthenticatedUser authenticatedUser;

        public UserRolesController(AuthenticatedUser authenticatedUser)
        {
            this.authenticatedUser = authenticatedUser;
        }

        public IActionResult Index(string userName)
        {
            var result = ApiRequestExtention.RequestGet<List<string>>(HttpContext, $"api/v1/UserRoles/GetRolesByUserName?userName={userName}").Data;
            return View(result);
        }

        //public IActionResult Create(string roleName)
        //{
        //    return View(new RoleClaimDTO() { RoleName = roleName });
        //}
        //[HttpPost]
        //public IActionResult Create(RoleClaimDTO model)
        //{
        //    var result = ApiRequestExtention.RequestPost<string>(HttpContext, "api/v1/RoleClaims/CreateRoleClaim", model);
        //    if (result is not null && result.IsSuccess)
        //    {
        //        return RedirectToAction("Index", new { roleName = model.RoleName });
        //    }
        //    return View(model);
        //}
    }
}

