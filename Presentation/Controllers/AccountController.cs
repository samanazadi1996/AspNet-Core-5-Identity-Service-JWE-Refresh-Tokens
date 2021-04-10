using Entities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Presentation.Infrastracture;
using Presentation.Models;
using Services.JWTServices.Abstraction;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IJwtService jwtService;

        public AccountController(ILogger<AccountController> logger,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IJwtService jwtService
            )
        {
            _logger = logger;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.jwtService = jwtService;
        }

        public IActionResult Login(string urlCallBack)
        {
            var username = User.Identity.Name;
            return View(new SignInViewModel() { UrlCallBack = urlCallBack });
        }
        [HttpPost]
        public async Task<IActionResult> Login(SignInViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var user = await userManager.FindByNameAsync(model.UserName);
            if (user is null)
                ModelState.AddModelError(nameof(model.UserName), "نام کاربری اشتباه است");

            var IsPasswordValid = await userManager.CheckPasswordAsync(user, model.Password);
            if (!IsPasswordValid)
                ModelState.AddModelError(nameof(model.UserName), "رمز عبور اشتباه است");

            var token = await jwtService.GenerateAsync(user);
            if (string.IsNullOrEmpty(token))
                return BadRequest();

            if (ModelState.IsValid)
            {
                var refreshtoken = "salam";
                NameValueCollection datacollection = new NameValueCollection();
                datacollection.Add("token", token);
                datacollection.Add("refreshtoken", refreshtoken);
                var str = FormPostExtention.PreparePostForm(model.UrlCallBack, datacollection);
                return Content(str, "text/html");
            }

            return View(model);
        }
        public IActionResult aaaa()
        {
            var user = new ApplicationUser() { Email = "admin@admin.com", UserName = "saman", PhoneNumber = "09304241296" };
            var op = userManager.CreateAsync(user, "123456").Result;

            return RedirectToAction("login");
        }
    }
}
