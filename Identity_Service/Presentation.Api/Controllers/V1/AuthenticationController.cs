using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Presentation.Api.Infrastracture;
using Presentation.Api.Models.Account;
using Services.JWTDomain.Abstraction;
using Services.RefreshTokenDomain.Abstraction;
using System;
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
    public class AuthenticationController : ControllerBase
    {
        #region Constructor
        private readonly IGetClaimsByTokenService getClaimsByTokenService;
        private readonly IGetRefreshTokenService getRefreshTokenService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly IJwtService jwtService;
        private readonly IUpdateResreshTokenService updateResreshTokenService;


        public AuthenticationController(IGetClaimsByTokenService getClaimsByTokenService, IGetRefreshTokenService getRefreshTokenService, UserManager<ApplicationUser> userManager, IJwtService jwtService, IUpdateResreshTokenService updateResreshTokenService, RoleManager<ApplicationRole> roleManager)
        {
            this.getClaimsByTokenService = getClaimsByTokenService;
            this.getRefreshTokenService = getRefreshTokenService;
            this.userManager = userManager;
            this.jwtService = jwtService;
            this.updateResreshTokenService = updateResreshTokenService;
            this.roleManager = roleManager;
        }
        #endregion

        #region Authentication
        [HttpGet("Authenticate/{token}")]
        public ApiResult<ResponseAuthorizeDTO> Authenticate(string token)
        {
            var result = getClaimsByTokenService.Get(token);
            if (result is null)
            {
                return BadRequest();
            }

            var response = new ResponseAuthorizeDTO()
            {
                name = result.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value,
                userId = result.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value,
                roles = result.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value),
            };

            return Ok(response);
        }

        [HttpGet("RefreshToken/{refreshToken}")]
        public async Task<ApiResult<TokensDTO>> RefreshToken(Guid refreshToken)
        {
            var myIp = GetCurrentIpAddressExtention.Get(HttpContext);

            var refreshTokenModel = await getRefreshTokenService.GetByRefreshToken(refreshToken);
            if (refreshTokenModel is null || refreshTokenModel.IsExpired)
            {
                return BadRequest();
            }
            var newRefreshToken = await updateResreshTokenService.Update(refreshToken, myIp);
            var user = await userManager.FindByIdAsync(refreshTokenModel?.UserId);
            var newJWT = await jwtService.GenerateAsync(user);

            return Ok(new TokensDTO() { token = newJWT, refreshToken = newRefreshToken });
        }
        #endregion
    }
}
