using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Presentation.Api.Models;
using Presentation.Api.Models.Account;
using Presentation.Api.Models.UserClaims;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebFramework.Api;
using WebFramework.Filters;

namespace Presentation.Api.Controllers.V1
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
        public async Task<ApiResult<IEnumerable<string>>> GetUserClaims(string userName)
        {
            var user = await userManager.FindByNameAsync(userName);
            var result = await userManager.GetClaimsAsync(user);
            return Ok(result.Select(p => p.Value));
        }

        [HttpPost]
        public async Task<ApiResult> CreateUserClaims(UserClaimsDTO model)
        {
            var user = await userManager.FindByNameAsync(model.UserName);
            var result = await userManager.AddClaimsAsync(user, model.Claims.Select(p => new Claim(p.type, p.value)));

            if (result.Succeeded)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpPost]
        public async Task<ApiResult> CreateUserClaim(UserClaimDTO model)
        {
            var user = await userManager.FindByNameAsync(model.UserName);
            var result = await userManager.AddClaimAsync(user, new Claim(model.Claim.type, model.Claim.value));

            if (result.Succeeded)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpDelete]
        public async Task<ApiResult> RemoveClaim(string userName, string claimValue)
        {
            var user = await userManager.FindByNameAsync(userName);
            var claim = (await userManager.GetClaimsAsync(user)).FirstOrDefault(c => c.Value.Equals(claimValue));
            var result = await userManager.RemoveClaimAsync(user, claim);
            if (result.Succeeded)
            {
                return Ok();
            }

            return BadRequest();
        }

    }
}
