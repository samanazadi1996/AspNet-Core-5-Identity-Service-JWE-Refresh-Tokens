using Microsoft.AspNetCore.Mvc;
using Presentation.WebUI.Infrastructure;
using Presentation.WebUI.Infrastructure.Authentication;
using Presentation.WebUI.Infrastructure.Authentication.Attributes;
using Presentation.WebUI.Models;
using System.Collections.Generic;
using System.Linq;

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
            ViewBag.userName = userName;
            var result = ApiRequestExtention.RequestGet<List<string>>(HttpContext, $"api/v1/UserRoles/GetRolesByUserName?userName={userName}").Data;
            return View(result);
        }

        public IActionResult Create(string userName)
        {
            ViewBag.roles = ApiRequestExtention.RequestGet<List<SelectListDTO>>(HttpContext, "api/v1/Role/GetRoles").Data;
            return View(new UserRoleDTO { UserName = userName });
        }
        [HttpPost]
        public IActionResult Create(UserRoleDTO model)
        {
            var result = ApiRequestExtention.RequestPost<string>(HttpContext, "api/v1/UserRoles/AddRoleToUser", model);
            if (result is not null && result.IsSuccess)
            {
                return RedirectToAction("Index", new { userName = model.UserName });
            }

            ViewBag.roles = ApiRequestExtention.RequestGet<List<SelectListDTO>>(HttpContext, "api/v1/Role/GetRoles").Data;
            return View(model);
        }

        [HttpGet]
        public IActionResult RemoveUserRole(string userName, string roleName)
        {
            ApiRequestExtention.RequestDelete<string>(HttpContext, $"api/v1/UserRoles/RemoveUserRole?username={userName}&roleName={roleName}");
            return RedirectToAction("Index", new { userName = userName });
        }

    }
}

