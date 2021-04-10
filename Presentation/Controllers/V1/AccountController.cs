using Entities;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;
using System.Collections.Generic;
using WebFramework.Filters;

namespace Presentation.Controllers.V1
{
    [ApiVersion("1")]
    [ApiController]
    [ApiResultFilter]
    [Route("api/v1/[controller]/[action]")]
    public class AccountController : ControllerBase
    {
        [HttpPost]
        public string Get(TokensDTO model)
        {
            return "asfdasda";
        }

    }
}
