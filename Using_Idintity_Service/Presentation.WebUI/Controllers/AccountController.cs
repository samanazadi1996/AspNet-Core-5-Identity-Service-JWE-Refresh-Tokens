using Identity.Client.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Presentation.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApiAuthenticationOptions options;

        public AccountController(ILogger<HomeController> logger, ApiAuthenticationOptions options)
        {
            _logger = logger;
            this.options = options;
        }

        public IActionResult Login()
        {
            string key = "tJi7v+n40ToUVbuNUU1MOU5DoUZNW3pSTS0JJyESgw3CXob/NSjz2KVJBfnV2hZG";

            return Redirect($"{options.Domain}account/login?UCB={key}");
        }

        [HttpPost]
        public IActionResult SetDataLogin(TokensDTO logindto)
        {
            HttpContext.Session.SetString("refreshtoken", logindto.refreshToken);
            HttpContext.Session.SetString("token", logindto.token);
            return Redirect("/");
        }

    }
}
