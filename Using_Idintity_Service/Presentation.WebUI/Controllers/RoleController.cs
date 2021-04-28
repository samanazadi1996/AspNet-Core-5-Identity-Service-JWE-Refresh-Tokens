using Identity.Client.Attributes;
using Identity.Client.Services.ApiRequest;
using Microsoft.AspNetCore.Mvc;
using Presentation.WebUI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Presentation.WebUI.Controllers
{
    public class RoleController : Controller
    {
        private readonly IApiRequestService apiRequestService;


        public RoleController(IApiRequestService apiRequestService)
        {
            this.apiRequestService = apiRequestService;
        }
        [ApiAuthorize]
        public IActionResult Index()
        {
            var result = apiRequestService.RequestGet<List<SelectListDTO>>("api/v1/Role/GetRoles").Result.Data;
            return View(result);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(RoleDTO role)
        {
            var result = apiRequestService.RequestPost<RoleDTO>("api/v1/Role/CreateRole", role).Result;
            if (result is not null && result.IsSuccess)
            {
                return RedirectToAction("Index");
            }
            return View(role);
        }

        [HttpGet]
        public async Task<IActionResult> RemoveRole(string roleName)
        {
            await apiRequestService.RequestDelete($"api/v1/Role/RemoveRole?roleName={roleName}");
            return RedirectToAction("Index");
        }

    }
}

