using Microsoft.AspNetCore.Mvc;
using Presentation.WebUI.Infrastructure;
using Presentation.WebUI.Infrastructure.Authentication;
using Presentation.WebUI.Infrastructure.Authentication.Attributes;
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

            return View(result);
        }

        public IActionResult Create(string userName)
        {
            return View(new UserClaimsDTO() { userName = userName });
        }
        [HttpPost]
        public IActionResult Create(UserClaimsDTO model)
        {
            var result = ApiRequestExtention.RequestPost<string>(HttpContext, "api/v1/UserClaims/CreateUserClaims", model);
            if (result is not null && result.IsSuccess)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}

