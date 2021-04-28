using Identity.Client;
using Identity.Client.Attributes;
using Identity.Client.Services.ApiRequest;
using Microsoft.AspNetCore.Mvc;
using Presentation.WebUI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Presentation.WebUI.Controllers
{
    [ApiAuthorize]
    public class UserClaimsController : Controller
    {
        private readonly AuthenticatedUser authenticatedUser;
        private readonly IApiRequestService apiRequestService;

        public UserClaimsController(AuthenticatedUser authenticatedUser, IApiRequestService apiRequestService)
        {
            this.authenticatedUser = authenticatedUser;
            this.apiRequestService = apiRequestService;
        }
        public IActionResult Index(string userName)
        {
            var result = apiRequestService.RequestGet<List<string>>($"api/v1/UserClaims/GetUserClaims?userName={userName}").Result.Data;
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

            var result = apiRequestService.RequestPost<string>("api/v1/UserClaims/CreateUserClaim", model).Result;
            if (result is not null && result.IsSuccess)
            {
                return RedirectToAction("Index", new { userName = model.UserName });
            }
            return View(model);
        }
        public async Task<IActionResult> RemoveClaim(string userName, string claimValue)
        {
            await apiRequestService.RequestDelete($"api/v1/UserClaims/RemoveClaim?username={userName}&claimValue={claimValue}");
            return RedirectToAction("Index", new { userName = userName });
        }

    }
}

