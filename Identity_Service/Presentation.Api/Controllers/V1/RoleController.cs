using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Presentation.Api.Models.Common;
using Presentation.Api.Models.Role;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebFramework.Api;
using WebFramework.Filters;

namespace Presentation.Api.Controllers.V1
{
    [ApiVersion("1")]
    [ApiController]
    [ApiResultFilter]
    [Route("api/v1/[controller]/[action]")]
    public class RoleController : ControllerBase
    {
        private readonly RoleManager<ApplicationRole> roleManager;

        public RoleController(RoleManager<ApplicationRole> roleManager)
        {
            this.roleManager = roleManager;
        }

        [HttpGet]
        public ApiResult<IEnumerable<SelectListDTO>> GetRoles()
        {
            var result = roleManager.Roles.Select(p => new SelectListDTO { Id = p.Id, Name = p.Name }).AsEnumerable();
            return Ok(result);
        }

        [HttpPost]
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

        [HttpDelete]
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
    }
}
