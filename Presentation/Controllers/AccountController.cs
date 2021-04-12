using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Presentation.Infrastracture;
using Presentation.Models;
using Services.JWTDomain.Abstraction;
using Services.RefreshTokenDomain.Abstraction;
using System;
using System.Collections.Specialized;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IJwtService jwtService;
        private readonly IGenerateResreshTokenService generateResreshTokenService;

        public AccountController(ILogger<AccountController> logger,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IJwtService jwtService,
            IGenerateResreshTokenService generateResreshTokenService)
        {
            _logger = logger;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.jwtService = jwtService;
            this.generateResreshTokenService = generateResreshTokenService;
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

            if (ModelState.IsValid)
            {
                var myIp = GetCurrentIpAddressExtention.Get(HttpContext);
                var token = await jwtService.GenerateAsync(user);
                var RefreshtokenGuid = await generateResreshTokenService.Generate(user, myIp);
                var refreshtoken = Convert.ToString(RefreshtokenGuid);
                NameValueCollection datacollection = new NameValueCollection();
                datacollection.Add("token", token);
                datacollection.Add("refreshtoken", refreshtoken);
                var str = FormPostExtention.PreparePostForm(model.UrlCallBack, datacollection);
                return Content(str, "text/html");
            }

            return View(model);
        }
        public async Task<IActionResult> qqq(SignInViewModel model)
        {
            var user = new ApplicationUser() { Email = "saman@admin.com", UserName = "saman", PhoneNumber = "09123456789" };
            await userManager.CreateAsync(user, "123456");
            await roleManager.CreateAsync(new IdentityRole() { Id = "admin", Name = "admin" });
            await roleManager.CreateAsync(new IdentityRole() { Id = "developer", Name = "developer" });
            await roleManager.CreateAsync(new IdentityRole() { Id = "customer", Name = "customer" });
            await userManager.AddToRoleAsync(user, "admin");
            await userManager.AddToRoleAsync(user, "developer");

            return RedirectToAction("login");
        }
    }
}
