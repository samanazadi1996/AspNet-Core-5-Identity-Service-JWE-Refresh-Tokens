using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Presentation.Infrastracture;
using Presentation.Models.Account;
using Presentation.Models.Email;
using Presentation.Models.ResetPassword;
using Services.CryptographyDomain.Abstraction;
using Services.JWTDomain.Abstraction;
using Services.MailDomain.Abstraction;
using Services.RefreshTokenDomain.Abstraction;
using System;
using System.Collections.Specialized;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly IJwtService jwtService;
        private readonly IGenerateResreshTokenService generateResreshTokenService;
        private readonly IDecryptService decryptService;
        private readonly IEncryptService encryptService;
        private readonly ISendEmailService sendEmailService;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            IJwtService jwtService,
            IGenerateResreshTokenService generateResreshTokenService,
            IDecryptService decryptService,
            IEncryptService encryptService,
            ISendEmailService sendEmailService, RoleManager<ApplicationRole> roleManager)
        {
            this.userManager = userManager;
            this.jwtService = jwtService;
            this.generateResreshTokenService = generateResreshTokenService;
            this.decryptService = decryptService;
            this.encryptService = encryptService;
            this.sendEmailService = sendEmailService;
            this.roleManager = roleManager;
        }

        public IActionResult Login(string Key)
        {
            return View(new SignInViewModel() { Key = Key });
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
                ModelState.AddModelError(nameof(model.Password), "رمز عبور اشتباه است");

            if (ModelState.IsValid)
            {
                var myIp = GetCurrentIpAddressExtention.Get(HttpContext);
                var token = await jwtService.GenerateAsync(user);
                var RefreshtokenGuid = await generateResreshTokenService.Generate(user, myIp);
                NameValueCollection datacollection = new NameValueCollection();
                datacollection.Add("token", token);
                datacollection.Add("refreshtoken", Convert.ToString(RefreshtokenGuid));
                var urlCallBack = decryptService.Decrypt(model.Key);
                var form = FormPostExtention.PreparePostForm(urlCallBack, datacollection);
                return Content(form, "text/html");
            }

            return View(model);
        }

        public String getEncriptedUrl(string url)
        {
            var result = encryptService.Encrypt(url);
            return result;
        }
        public IActionResult ResponseForgotPassword()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            var model = new ForgotPasswordDTO()
            {
                sessionId = DateTime.Now.Ticks
            };

            return View(model);
        }
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDTO ForgotPassword, string txtCaptcha)
        {
            if (ModelState.IsValid)
            {

                var user = await userManager.FindByEmailAsync(ForgotPassword.UserName);
                if (user is null)
                {
                    ModelState.AddModelError("UserName", "  کاربری با این مشخصات یافت نشد .");
                    return View(ForgotPassword);
                }

                //var captchaText = TextTools.GetEnglishNumber(txtCaptcha);
                //var session = HttpContext.Session.GetString("Captcha" + ForgotPassword.sessionId);
                //if (string.IsNullOrEmpty(session) || session != captchaText)
                //{
                //    ModelState.AddModelError("", "کد امنیتی را اشتباه وارد کردید");
                //}

                var emailContent = new EmailContentDto()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Token = await userManager.GeneratePasswordResetTokenAsync(user),
                };
                var Body = await this.RenderViewAsync("_ResetPasswordEmailContent", emailContent);

                await sendEmailService.SendAsync(user.Email, Body);

                return RedirectToAction("ResponseForgotPassword");
            }

            return View(ForgotPassword);
        }

        public IActionResult ResetPassword(string username, string token)
        {
            return View(new ResetPasswordDTO()
            {
                UserName = username,
                Token = token
            });
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordDTO model)
        {
            var user = await userManager.FindByNameAsync(model.UserName);

            if (user is null)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var result = await userManager.ResetPasswordAsync(user, model.Token, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Login");
                }

                return View();
            }
            return BadRequest();

        }

        public async Task<IActionResult> qqq(SignInViewModel model)
        {
            var user = new ApplicationUser() { Email = "saman@admin.com", UserName = "saman_azadi", FirstName = "Saman", LastName = "Azadi", PhoneNumber = "09123456789" };
            await userManager.CreateAsync(user, "123456");
            await roleManager.CreateAsync(new ApplicationRole() { Id = "admin", Name = "admin" });
            await roleManager.CreateAsync(new ApplicationRole() { Id = "developer", Name = "developer" });
            await roleManager.CreateAsync(new ApplicationRole() { Id = "customer", Name = "customer" });
            await userManager.AddToRoleAsync(user, "admin");
            await userManager.AddToRoleAsync(user, "developer");

            return RedirectToAction("login");
        }
    }
}
