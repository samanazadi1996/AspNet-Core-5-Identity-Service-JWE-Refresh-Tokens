using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Presentation.Api.Models.Common;
using Presentation.Api.Models.Role;
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
    [Route("api/v1/[controller]")]
    public class RoleController : ControllerBase
    {
        #region Constructor
        private readonly RoleManager<ApplicationRole> roleManager;

        public RoleController(RoleManager<ApplicationRole> roleManager)
        {
            this.roleManager = roleManager;
        }
        #endregion

        #region Role
        [HttpGet("Roles")]
        public ApiResult<IEnumerable<SelectListDTO>> GetRoles()
        {
            var result = roleManager.Roles.Select(p => new SelectListDTO { Id = p.Id, Name = p.Name }).AsEnumerable();
            return Ok(result);
        }

        [HttpPost("Roles/Create")]
        public async Task<ApiResult<RoleDTO>> CreateRole(RoleDTO model)
        {
            var role = new ApplicationRole()
            {
                Id = model.Id,
                Name = model.Name
            };
            var result = await roleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                return Ok(value: new RoleDTO { Id = role.Id, Name = role.Name });
            }
            return BadRequest();

        }

        [HttpDelete("Role/{roleName}/Delete")]
        public async Task<ApiResult> RemoveRole(string roleName)
        {
            var role = await roleManager.FindByNameAsync(roleName);
            var result = await roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                return Ok();
            }
            return BadRequest();
        }
        #endregion
        #region RoleClaims
        [HttpGet("Role/{roleName}/Claims")]
        public async Task<ApiResult<IEnumerable<string>>> GetRoleClaims(string roleName)
        {
            var role = await roleManager.FindByNameAsync(roleName);
            var result = await roleManager.GetClaimsAsync(role);
            return Ok(result.Select(p => p.Value));
        }

        [HttpPost("Role/{roleName}/Claims/Create")]
        public async Task<ApiResult> CreateRoleClaim(string roleName, ClaimDTO model)
        {
            var user = await roleManager.FindByNameAsync(roleName);
            var result = await roleManager.AddClaimAsync(user, new Claim(model.type, model.value));

            if (result.Succeeded)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpDelete("Role/{roleName}/Claim/{claimValue}/Delete")]
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
        #endregion
    }
}
