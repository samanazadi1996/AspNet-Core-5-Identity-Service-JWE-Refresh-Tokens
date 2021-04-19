using Microsoft.AspNetCore.Mvc;
using Presentation.WebUI.Infrastructure;
using Presentation.WebUI.Infrastructure.Authentication;
using Presentation.WebUI.Infrastructure.Authentication.Attributes;
using Presentation.WebUI.Models;
using System.Collections.Generic;

namespace Presentation.WebUI.Controllers
{
    [ApiAuthorize]
    public class RoleClaimsController : Controller
    {
        private readonly AuthenticatedUser authenticatedUser;

        public RoleClaimsController(AuthenticatedUser authenticatedUser)
        {
            this.authenticatedUser = authenticatedUser;
        }
        public IActionResult Index(string roleName)
        {
            var result = ApiRequestExtention.RequestGet<List<string>>(HttpContext, $"api/v1/RoleClaims/GetRoleClaims?roleName={roleName}").Data;
            ViewBag.roleName = roleName;
            return View(result);
        }

        public IActionResult Create(string roleName)
        {
            return View(new RoleClaimDTO() { RoleName = roleName });
        }
        [HttpPost]
        public IActionResult Create(RoleClaimDTO model)
        {
            var result = ApiRequestExtention.RequestPost<string>(HttpContext, "api/v1/RoleClaims/CreateRoleClaim", model);
            if (result is not null && result.IsSuccess)
            {
                return RedirectToAction("Index", new { roleName = model.RoleName });
            }
            return View(model);
        }
    }
}

