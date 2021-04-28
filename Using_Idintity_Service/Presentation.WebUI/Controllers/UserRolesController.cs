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
    public class UserRolesController : Controller
    {
        private readonly AuthenticatedUser authenticatedUser;
        private readonly IApiRequestService apiRequestService;

        public UserRolesController(AuthenticatedUser authenticatedUser, IApiRequestService apiRequestService)
        {
            this.authenticatedUser = authenticatedUser;
            this.apiRequestService = apiRequestService;
        }

        public IActionResult Index(string userName)
        {
            ViewBag.userName = userName;
            var result = apiRequestService.RequestGet<List<string>>($"api/v1/UserRoles/GetRolesByUserName?userName={userName}").Result.Data;
            return View(result);
        }

        public IActionResult Create(string userName)
        {
            ViewBag.roles = apiRequestService.RequestGet<List<SelectListDTO>>("api/v1/Role/GetRoles").Result.Data;
            return View(new UserRoleDTO { UserName = userName });
        }
        [HttpPost]
        public IActionResult Create(UserRoleDTO model)
        {
            var result = apiRequestService.RequestPost<string>("api/v1/UserRoles/AddRoleToUser", model).Result;
            if (result is not null && result.IsSuccess)
            {
                return RedirectToAction("Index", new { userName = model.UserName });
            }

            ViewBag.roles = apiRequestService.RequestGet<List<SelectListDTO>>("api/v1/Role/GetRoles").Result.Data;
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> RemoveUserRole(string userName, string roleName)
        {
            await apiRequestService.RequestDelete($"api/v1/UserRoles/RemoveUserRole?username={userName}&roleName={roleName}");
            return RedirectToAction("Index", new { userName = userName });
        }

    }
}

