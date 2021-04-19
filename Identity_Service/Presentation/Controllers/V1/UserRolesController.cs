using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models.Role;
using Presentation.Models.UserRoles;
using System.Threading.Tasks;
using WebFramework.Filters;

namespace Presentation.Controllers.V1
{
    [ApiVersion("1")]
    [ApiController]
    [ApiResultFilter]
    [Route("api/v1/[controller]/[action]")]
    [Authorize]
    public class UserRolesController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public UserRolesController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetRolesByUserName(string userName)
        {
            var user = await userManager.FindByNameAsync(userName);
            var roles = await userManager.GetRolesAsync(user);
            return Ok(roles);
        }

        [HttpPost]
        public async Task<IActionResult> AddRoleToUser(CreateUserRoleDTO model)
        {
            var user = await userManager.FindByNameAsync(model.UserName);
            var result = await userManager.AddToRoleAsync(user, model.RoleName);

            if (result.Succeeded)
            {
                return Ok();
            }
            return BadRequest();
        }

    }
}
