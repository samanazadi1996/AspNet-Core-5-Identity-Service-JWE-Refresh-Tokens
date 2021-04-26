using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;
using Presentation.Models.Account;
using Presentation.Models.UserClaims;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebFramework.Api;
using WebFramework.Filters;

namespace Presentation.Controllers.V1
{
    [ApiVersion("1")]
    [ApiController]
    [ApiResultFilter]
    [Route("api/v1/[controller]/[action]")]
    public class RoleClaimsController : ControllerBase
    {
        private readonly RoleManager<ApplicationRole> roleManager;

        public RoleClaimsController(RoleManager<ApplicationRole> roleManager)
        {
            this.roleManager = roleManager;
        }

        [HttpGet]
        public async Task<ApiResult<IEnumerable<string>>> GetRoleClaims(string roleName)
        {
            var role = await roleManager.FindByNameAsync(roleName);
            var result = await roleManager.GetClaimsAsync(role);
            return Ok(result.Select(p => p.Value));
        }

        [HttpPost]
        public async Task<ApiResult> CreateRoleClaim(RoleClaimDTO model)
        {
            var user = await roleManager.FindByNameAsync(model.RoleName);
            var result = await roleManager.AddClaimAsync(user, new Claim(model.Claim.type, model.Claim.value));

            if (result.Succeeded)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpDelete]
        public async Task<ApiResult> RemoveClaim(string roleName, string claimValue)
        {
            var role = await roleManager.FindByNameAsync(roleName);
            var claim = (await roleManager.GetClaimsAsync(role)).FirstOrDefault(c => c.Value.Equals(claimValue));
            var result = await roleManager.RemoveClaimAsync(role, claim);
            if (result.Succeeded)
            {
                return Ok();
            }

            return BadRequest();
        }

    }
}
