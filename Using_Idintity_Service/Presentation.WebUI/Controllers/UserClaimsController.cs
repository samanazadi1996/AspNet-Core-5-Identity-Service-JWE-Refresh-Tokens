﻿using Identity.Client;
using Identity.Client.Attributes;
using Microsoft.AspNetCore.Mvc;
using Presentation.WebUI.Infrastructure;
using Presentation.WebUI.Models;
using System.Collections.Generic;

namespace Presentation.WebUI.Controllers
{
    [ApiAuthorize]
    public class UserClaimsController : Controller
    {
        private readonly AuthenticatedUser authenticatedUser;

        public UserClaimsController(AuthenticatedUser authenticatedUser)
        {
            this.authenticatedUser = authenticatedUser;
        }
        public IActionResult Index(string userName)
        {
            var result = ApiRequestExtention.RequestGet<List<string>>(HttpContext, $"api/v1/UserClaims/GetUserClaims?userName={userName}").Data;
            ViewBag.userName = userName;
            return View(result);
        }

        public IActionResult Create(string userName)
        {
            return View(new UserClaimDTO() { UserName = userName });
        }
        [HttpPost]
        public IActionResult Create(UserClaimDTO model)
        {

            var result = ApiRequestExtention.RequestPost<string>(HttpContext, "api/v1/UserClaims/CreateUserClaim", model);
            if (result is not null && result.IsSuccess)
            {
                return RedirectToAction("Index", new { userName = model.UserName });
            }
            return View(model);
        }
        public IActionResult RemoveClaim(string userName, string claimValue)
        {
            ApiRequestExtention.RequestDelete<string>(HttpContext, $"api/v1/UserClaims/RemoveClaim?username={userName}&claimValue={claimValue}");
            return RedirectToAction("Index", new { userName = userName });
        }

    }
}

