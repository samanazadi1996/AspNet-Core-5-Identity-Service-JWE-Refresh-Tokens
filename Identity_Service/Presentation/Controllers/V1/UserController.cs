using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;
using System.Linq;
using WebFramework.Filters;

namespace Presentation.Controllers.V1
{
    [ApiVersion("1")]
    [ApiController]
    [ApiResultFilter]
    [Route("api/v1/[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;

        public UserController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetUsers()
        {
            var result = userManager.Users.Select(p => new SelectListDTO { Value = p.Id, Text = p.UserName }).AsEnumerable();
            return Ok(result);
        }
    }
}
