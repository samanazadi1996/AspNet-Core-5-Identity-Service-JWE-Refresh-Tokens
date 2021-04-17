using Common;
using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Services.JWTDomain.Abstraction;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services.JWTDomain.Implementation
{
    public class JwtService : IJwtService
    {
        private readonly SiteSettings _SiteSetting;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public JwtService(IOptionsSnapshot<SiteSettings> Setting, SignInManager<ApplicationUser> signInManager)
        {
            _SiteSetting = Setting.Value;
            _signInManager = signInManager;
        }

        public async Task<string> GenerateAsync(ApplicationUser user)
        {
            var issuer = _SiteSetting.JwtSettings.Issuer;
            var audienceId = _SiteSetting.JwtSettings.Audience;
            var audienceSecret = Encoding.UTF8.GetBytes(_SiteSetting.JwtSettings.SecretKey);
            var encryptionkey = Encoding.UTF8.GetBytes(_SiteSetting.JwtSettings.Encryptkey); //must be 16 character
            var encryptingCredentials = new EncryptingCredentials(new SymmetricSecurityKey(encryptionkey), SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256);

            var secretKey = audienceSecret; // longer that 16 character
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature);


            var claims = await _getClaimsAsync(user);

            if (claims is null)
                return null;

            var descriptor = new SecurityTokenDescriptor
            {
                Issuer = issuer,
                Audience = audienceId,
                IssuedAt = DateTime.Now,
                NotBefore = DateTime.Now.AddMinutes(_SiteSetting.JwtSettings.NotBeforeMinutes),
                Expires = DateTime.Now.AddMinutes(_SiteSetting.JwtSettings.ExpirationMinutes),
                SigningCredentials = signingCredentials,
                EncryptingCredentials = encryptingCredentials,
                Subject = new ClaimsIdentity(claims)
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var securityToken = tokenHandler.CreateToken(descriptor);

            var jwt = tokenHandler.WriteToken(securityToken);

            return jwt;
        }
        private async Task<IEnumerable<Claim>> _getClaimsAsync(ApplicationUser user)
        {
            var result = await _signInManager.ClaimsFactory.CreateAsync(user);
            return result.Claims;
        }
    }
}
