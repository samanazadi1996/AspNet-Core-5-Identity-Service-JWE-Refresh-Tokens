using Identity.Client.Attributes;
using Identity.Client.Services.ApiRequest;
using Microsoft.AspNetCore.Mvc;
using Presentation.WebUI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Presentation.WebUI.Controllers
{
    public class UserController : Controller
    {
        public readonly IApiRequestService apiRequestService;

        public UserController(IApiRequestService apiRequestService)
        {
            this.apiRequestService = apiRequestService;
        }

        [ApiAuthorize]
        public IActionResult Index()
        {
            var result = apiRequestService.RequestGet<List<UserDTO>>("api/v1/User/GetUsers").Result;
            if (result.IsSuccess)
            {
                return View(result.Data);
            }
            return View(new List<UserDTO>());
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(UserDTO user)
        {
            var result = apiRequestService.RequestPost<UserDTO>("api/v1/User/CreateUser", user).Result;
            if (result is not null && result.IsSuccess)
            {
                return RedirectToAction("Index");
            }
            return View(user);
        }

        [HttpGet]
        public async Task<IActionResult> RemoveUser(string userName)
        {
            await apiRequestService.RequestDelete<string>($"api/v1/User/RemoveUser?userName={userName}");
            return RedirectToAction("Index");
        }

    }
}

