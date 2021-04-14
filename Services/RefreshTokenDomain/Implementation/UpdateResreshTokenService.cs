using Data.Repositores.Abstraction;
using Entities;
using Services.RefreshTokenDomain.Abstraction;
using System;
using System.Threading.Tasks;

namespace Services.RefreshTokenDomain.Implementation
{
    public class UpdateResreshTokenService : IUpdateResreshTokenService
    {
        private readonly IRefreshTokenRepository refreshTokenRepository;

        public UpdateResreshTokenService(IRefreshTokenRepository refreshTokenRepository)
        {
            this.refreshTokenRepository = refreshTokenRepository;
        }
        public async Task<Guid> Update(Guid token, string ipAddress)
        {
            var newToken = Guid.NewGuid();
            var refreshTokens = await refreshTokenRepository.GetBytoken(token);

            refreshTokens.Token = newToken;
            refreshTokens.ExpireDateTime = DateTime.Now.AddDays(1);

            await refreshTokenRepository.SaveAsync();
            return newToken;
        }
    }
}
