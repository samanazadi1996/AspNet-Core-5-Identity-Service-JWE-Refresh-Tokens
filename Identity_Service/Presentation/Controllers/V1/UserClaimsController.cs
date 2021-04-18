using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;
using Presentation.Models.Account;
using Presentation.Models.UserClaims;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebFramework.Filters;

namespace Presentation.Controllers.V1
{
    [ApiVersion("1")]
    [ApiController]
    [ApiResultFilter]
    [Route("api/v1/[controller]/[action]")]
    public class UserClaimsController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;

        public UserClaimsController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserClaims(string userName)
        {
            var user = await userManager.FindByNameAsync(userName);
            var result = await userManager.GetClaimsAsync(user);
            return Ok(result.Select(p => p.Value));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateUserClaims(UserClaimsDTO model)
        {
            var user = await userManager.FindByNameAsync(model.userName);
            var result = await userManager.AddClaimsAsync(user, model.claims.Select(p => new Claim(p.type, p.value)));

            if (result.Succeeded)
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}
