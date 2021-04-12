using Data.Repositores.Abstraction;
using Entities;
using Services.RefreshTokenDomain.Abstraction;
using System;
using System.Threading.Tasks;

namespace Services.RefreshTokenDomain.Implementation
{
    public class GenerateResreshTokenService : IGenerateResreshTokenService
    {
        private readonly IRefreshTokenRepository refreshTokenRepository;

        public GenerateResreshTokenService(IRefreshTokenRepository refreshTokenRepository)
        {
            this.refreshTokenRepository = refreshTokenRepository;
        }
        public async Task<Guid> Generate(ApplicationUser user, string ipAddress)
        {
            var refreshTokenValue = Guid.NewGuid();
            if (user.RefreshTokenId is null)
            {
                user.RefreshToken = new RefreshToken()
                {
                    CreatedByIp = ipAddress,
                    CreateDateTime = DateTime.Now,
                    ExpireDateTime = DateTime.Now.AddDays(1),
                    Token = refreshTokenValue,
                    UserId = user.Id
                };
            }
            else
            {
                var result = await refreshTokenRepository.GetById(user.RefreshTokenId.Value);
                result.CreatedByIp = ipAddress;
                result.CreateDateTime = DateTime.Now;
                result.ExpireDateTime = DateTime.Now.AddDays(1);
                result.Token = refreshTokenValue;
            }
            await refreshTokenRepository.SaveAsync();
            return refreshTokenValue;
        }
    }
}
