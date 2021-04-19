using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models.Common;
using Presentation.Models.Role;
using System.Linq;
using System.Threading.Tasks;
using WebFramework.Filters;

namespace Presentation.Controllers.V1
{
    [ApiVersion("1")]
    [ApiController]
    [ApiResultFilter]
    [Route("api/v1/[controller]/[action]")]
    public class RoleController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public RoleController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetRoles()
        {
            var result = roleManager.Roles.Select(p => new SelectListDTO { Id = p.Id, Name = p.Name }).AsEnumerable();
            return Ok(result);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateRole(RoleDTO model)
        {
            var role = new IdentityRole()
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

    }
}
