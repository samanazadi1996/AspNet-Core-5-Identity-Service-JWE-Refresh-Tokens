using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Presentation.Api.Models.Common;
using Presentation.Api.Models.User;
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
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;

        public UserController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        [HttpGet]
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

        [HttpPost]
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

        [HttpDelete]
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

    }
}
