using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Presentation.Api.Models.Common;
using Presentation.Api.Models.User;
using Presentation.Api.Models.UserClaims;
using Presentation.Api.Models.UserRoles;
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
    public class UserController : ControllerBase
    {
        #region Constructor
        private readonly UserManager<ApplicationUser> userManager;
        public UserController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }
        #endregion

        #region User
        [HttpGet("Users")]
        public ApiResult<IEnumerable<UserDTO>> GetUsers()
        {
            var result = userManager.Users.Select(p => new UserDTO
            {
                Id = p.Id,
                UserName = p.UserName,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Email = p.Email,
                PhoneNumber = p.PhoneNumber
            }).AsEnumerable();
            return Ok(result);
        }

        [HttpPost("Create")]
        public async Task<ApiResult<UserDTO>> CreateUser(UserDTO model)
        {
            var user = new ApplicationUser()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.UserName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber
            };
            var result = await userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                return Ok(user);
            }
            return BadRequest();
        }

        [HttpDelete("User/{userName}/Delete")]
        public async Task<ApiResult> RemoveUser(string userName)
        {
            var user = await userManager.FindByNameAsync(userName);
            var result = await userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return Ok();
            }
            return BadRequest();
        }
        #endregion
        #region UserClaims
        [HttpGet("User/{userName}/Claims")]
        public async Task<ApiResult<IEnumerable<string>>> GetUserClaims(string userName)
        {
            var user = await userManager.FindByNameAsync(userName);
            var result = await userManager.GetClaimsAsync(user);
            return Ok(result.Select(p => p.Value));
        }

        [HttpPost("User/{userName}/Claims/CreateMany")]
        public async Task<ApiResult> CreateUserClaims(string userName, UserClaimsDTO model)
        {
            var user = await userManager.FindByNameAsync(userName);
            var result = await userManager.AddClaimsAsync(user, model.Claims.Select(p => new Claim(p.type, p.value)));

            if (result.Succeeded)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpPost("User/{userName}/Claims/Create")]
        public async Task<ApiResult> CreateUserClaim(string userName, ClaimDTO model)
        {
            var user = await userManager.FindByNameAsync(userName);
            var result = await userManager.AddClaimAsync(user, new Claim(model.type, model.value));

            if (result.Succeeded)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpDelete("User/{userName}/Claim/{claimValue}/Delete")]
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
        #endregion
        #region UserRoles
        [HttpGet("User/{userName}/Roles")]
        public async Task<ApiResult<IEnumerable<string>>> GetRolesByUserName(string userName)
        {
            var user = await userManager.FindByNameAsync(userName);
            var roles = await userManager.GetRolesAsync(user);
            return Ok(roles);
        }

        [HttpPost("User/{userName}/Roles/Create")]
        public async Task<ApiResult> AddRoleToUser(string userName, [FromBody] string RoleName)
        {
            var user = await userManager.FindByNameAsync(userName);
            var result = await userManager.AddToRoleAsync(user, RoleName);

            if (result.Succeeded)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpDelete("User/{userName}/Role/{roleName}/Delete")]
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
        #endregion
    }
}
