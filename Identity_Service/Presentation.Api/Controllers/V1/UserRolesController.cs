using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Presentation.Api.Models.Role;
using Presentation.Api.Models.UserRoles;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebFramework.Api;
using WebFramework.Filters;

namespace Presentation.Controllers.V1
{
    [ApiVersion("1")]
    [ApiController]
    [ApiResultFilter]
    [Route("api/v1/[controller]/[action]")]
    public class UserRolesController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;

        public UserRolesController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        [HttpGet]
        public async Task<ApiResult<IEnumerable<string>>> GetRolesByUserName(string userName)
        {
            var user = await userManager.FindByNameAsync(userName);
            var roles = await userManager.GetRolesAsync(user);
            return Ok(roles);
        }

        [HttpPost]
        public async Task<ApiResult> AddRoleToUser(UserRoleDTO model)
        {
            var user = await userManager.FindByNameAsync(model.UserName);
            var result = await userManager.AddToRoleAsync(user, model.RoleName);

            if (result.Succeeded)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete]
        public async Task<ApiResult> RemoveUserRole(string userName, string roleName)
        {
            var user = await userManager.FindByNameAsync(userName);
            var result = await userManager.RemoveFromRoleAsync(user, roleName);
            if (result.Succeeded)
            {
                return Ok();
            }

            return BadRequest();
        }

    }
}
