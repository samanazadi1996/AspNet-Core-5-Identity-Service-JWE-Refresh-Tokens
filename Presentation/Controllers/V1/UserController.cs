using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Presentation.Infrastracture;
using Presentation.Models;
using Services.JWTDomain.Abstraction;
using Services.RefreshTokenDomain.Abstraction;
using System;
using System.Collections.Generic;
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
    public class UserController : ControllerBase
    {

        public UserController()
        {
        }
        [HttpPost]
        public async Task<IActionResult> GetUsers(TokensDTO model)
        {

            return Ok("xcvbnm");
        }
    }
}
