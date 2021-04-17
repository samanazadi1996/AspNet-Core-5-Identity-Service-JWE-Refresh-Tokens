using Microsoft.AspNetCore.Mvc;
using Presentation.WebUI.Infrastructure;
using Presentation.WebUI.Models;
using System.Collections.Generic;

namespace Presentation.WebUI.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            var result = ApiRequestExtention.RequestGet<List<SelectListDTO>>(HttpContext, "https://localhost:44390/api/v1/User/GetUsers").Data;

            return View(result);
        }
    }
}

