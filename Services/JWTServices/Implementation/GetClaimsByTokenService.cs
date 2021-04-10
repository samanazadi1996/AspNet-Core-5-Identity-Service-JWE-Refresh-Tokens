using Common;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Services.JWTServices.Abstraction;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Services.JWTServices.Implementation
{
    public class GetClaimsByTokenService : IGetClaimsByTokenService
    {
        private readonly SiteSettings _SiteSetting;

        public GetClaimsByTokenService(IOptionsSnapshot<SiteSettings> Setting)
        {
            _SiteSetting = Setting.Value;
        }
        public ClaimsPrincipal Get(string token)
        {
            try
            {
                var secretkey = Encoding.UTF8.GetBytes(_SiteSetting.JwtSettings.SecretKey);
                var encryptionkey = Encoding.UTF8.GetBytes(_SiteSetting.JwtSettings.Encryptkey);

                var tokenHandler = new JwtSecurityTokenHandler();
                var validationParameters = new TokenValidationParameters
                {
                    ClockSkew = TimeSpan.Zero, // default: 5 min
                    RequireSignedTokens = true,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretkey),

                    RequireExpirationTime = true,
                    ValidateLifetime = true,

                    ValidateAudience = true, //default : false
                    ValidAudience = _SiteSetting.JwtSettings.Audience,

                    ValidateIssuer = true, //default : false
                    ValidIssuer = _SiteSetting.JwtSettings.Issuer,

                    TokenDecryptionKey = new SymmetricSecurityKey(encryptionkey)
                };
                var result = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
                return result;

            }
            catch (Exception)
            {
                return null;
            }

        }

    }
}
