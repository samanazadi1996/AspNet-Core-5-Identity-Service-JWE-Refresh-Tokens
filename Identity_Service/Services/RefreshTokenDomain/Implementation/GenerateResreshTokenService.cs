using Data.Repositores.Abstraction;
using Entities;
using Services.RefreshTokenDomain.Abstraction;
using System;
using System.Linq;
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
            var refreshToken = new RefreshToken()
            {
                CreatedByIp = ipAddress,
                CreateDateTime = DateTime.Now,
                ExpireDateTime = DateTime.Now.AddDays(1),
                Token = refreshTokenValue,
                UserId = user.Id
            };
            await refreshTokenRepository.Create(refreshToken);

            return refreshTokenValue;

        }
    }
}
