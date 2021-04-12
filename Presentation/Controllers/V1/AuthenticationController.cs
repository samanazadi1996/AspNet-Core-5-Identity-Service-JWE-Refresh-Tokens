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
    public class AuthenticationController : ControllerBase
    {
        private readonly IGetClaimsByTokenService getClaimsByTokenService;
        private readonly IGetRefreshTokenService getRefreshTokenService;
        private readonly IGenerateResreshTokenService generateResreshTokenService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IJwtService jwtService;


        public AuthenticationController(IGetClaimsByTokenService getClaimsByTokenService, IGetRefreshTokenService getRefreshTokenService, IGenerateResreshTokenService generateResreshTokenService, UserManager<ApplicationUser> userManager, IJwtService jwtService)
        {
            this.getClaimsByTokenService = getClaimsByTokenService;
            this.getRefreshTokenService = getRefreshTokenService;
            this.generateResreshTokenService = generateResreshTokenService;
            this.userManager = userManager;
            this.jwtService = jwtService;
        }
        [HttpPost]
        public async Task<IActionResult> Authenticate(TokensDTO model)
        {
            var result = getClaimsByTokenService.Get(model.token);
            var refreshToken = await getRefreshTokenService.GetByRefreshToken(model.refreshToken);
            var user = await userManager.FindByIdAsync(refreshToken?.UserId);

            var response = new ResponseAuthorizeDTO();
            if (result is null)
            {
                if (refreshToken is null || refreshToken.IsExpired)
                {
                    return BadRequest();
                }
                var newRefreshToken = await generateResreshTokenService.Generate(user, GetCurrentIpAddressExtention.Get(HttpContext));
                var newJWT = await jwtService.GenerateAsync(user);
                response.newData = new NewTokensDTO()
                {
                    refreshToken = Convert.ToString(newRefreshToken),
                    token = newJWT
                };
                result = getClaimsByTokenService.Get(newJWT);
            }

            if (result is null)
            {
                return BadRequest();
            }

            response.name = result.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value;
            response.userId = result.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            response.roles = string.Join(",", result.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value));

            return Ok(response);
        }
    }
}
