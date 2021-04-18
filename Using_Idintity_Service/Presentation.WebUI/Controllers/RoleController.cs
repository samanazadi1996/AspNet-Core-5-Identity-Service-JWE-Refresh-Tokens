using Microsoft.AspNetCore.Mvc;
using Presentation.WebUI.Infrastructure;
using Presentation.WebUI.Infrastructure.Authentication.Attributes;
using Presentation.WebUI.Models;
using System.Collections.Generic;

namespace Presentation.WebUI.Controllers
{
    public class RoleController : Controller
    {
        [ApiAuthorize]
        public IActionResult Index()
        {
            var result = ApiRequestExtention.RequestGet<List<SelectListDTO>>(HttpContext, "https://localhost:44390/api/v1/Role/GetRoles").Data;
            return View(result);
        }
    }
}

