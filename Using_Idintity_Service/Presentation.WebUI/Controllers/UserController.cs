using Microsoft.AspNetCore.Mvc;
using Presentation.WebUI.Infrastructure;
using Presentation.WebUI.Infrastructure.Authentication.Attributes;
using Presentation.WebUI.Models;
using System.Collections.Generic;

namespace Presentation.WebUI.Controllers
{
    public class UserController : Controller
    {
        [ApiAuthorize]
        public IActionResult Index()
        {
            var result = ApiRequestExtention.RequestGet<List<SelectListDTO>>(HttpContext, "https://localhost:44390/api/v1/User/GetUsers").Data;

            return View(result);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(UserDto user)
        {
            var result = ApiRequestExtention.RequestPost<UserDto>(HttpContext, "https://localhost:44390/api/v1/User/CreateUser", user);
            if (result is not null && result.IsSuccess)
            {
                return RedirectToAction("Index");
            }
            return View(user);
        }
    }
}

