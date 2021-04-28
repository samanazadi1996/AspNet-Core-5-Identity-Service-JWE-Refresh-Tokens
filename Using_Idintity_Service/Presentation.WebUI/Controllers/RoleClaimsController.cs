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
    public class RoleClaimsController : Controller
    {
        private readonly AuthenticatedUser authenticatedUser;
        private readonly IApiRequestService apiRequestService;

        public RoleClaimsController(AuthenticatedUser authenticatedUser, IApiRequestService apiRequestService)
        {
            this.authenticatedUser = authenticatedUser;
            this.apiRequestService = apiRequestService;
        }
        public async Task<IActionResult> Index(string roleName)
        {
            var result = (await apiRequestService.RequestGet<List<string>>($"api/v1/RoleClaims/GetRoleClaims?roleName={roleName}")).Data;
            ViewBag.roleName = roleName;
            return View(result);
        }

        public IActionResult Create(string roleName)
        {
            return View(new RoleClaimDTO() { RoleName = roleName });
        }
        [HttpPost]
        public async Task<IActionResult> Create(RoleClaimDTO model)
        {
            var result = await apiRequestService.RequestPost<string>("api/v1/RoleClaims/CreateRoleClaim", model);
            if (result is not null && result.IsSuccess)
            {
                return RedirectToAction("Index", new { roleName = model.RoleName });
            }
            return View(model);
        }
        public async Task<IActionResult> RemoveClaim(string roleName, string claimValue)
        {
            await apiRequestService.RequestDelete($"api/v1/RoleClaims/RemoveClaim?roleName={roleName}&claimValue={claimValue}");
            return RedirectToAction("Index", new { roleName = roleName });
        }

    }
}

