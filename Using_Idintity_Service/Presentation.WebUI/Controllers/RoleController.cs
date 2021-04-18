﻿using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(RoleDTO role)
        {
            var result = ApiRequestExtention.RequestPost<RoleDTO>(HttpContext, "https://localhost:44390/api/v1/Role/CreateRole", role);
            if (result is not null && result.IsSuccess)
            {
                return RedirectToAction("Index");
            }
            return View(role);
        }
    }
}

