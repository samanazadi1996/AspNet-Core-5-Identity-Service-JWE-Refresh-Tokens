using Microsoft.AspNetCore.Mvc;
using Presentation.WebUI.Infrastructure;
using Presentation.WebUI.Infrastructure.Authentication.Attributes;
using Presentation.WebUI.Models;
using System.Collections.Generic;

namespace Presentation.WebUI.Controllers
{
    public class RoleController : Controller
    {
        [ApiAuthorize("poi")]
        public IActionResult Index()
        {
            var result = ApiRequestExtention.RequestGet<List<SelectListDTO>>(HttpContext, "api/v1/Role/GetRoles").Data;
            return View(result);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(RoleDTO role)
        {
            var result = ApiRequestExtention.RequestPost<RoleDTO>(HttpContext, "api/v1/Role/CreateRole", role);
            if (result is not null && result.IsSuccess)
            {
                return RedirectToAction("Index");
            }
            return View(role);
        }

        [HttpGet]
        public IActionResult RemoveRole(string roleName)
        {
            ApiRequestExtention.RequestDelete<string>(HttpContext, $"api/v1/Role/RemoveRole?roleName={roleName}");
            return RedirectToAction("Index");
        }

    }
}

