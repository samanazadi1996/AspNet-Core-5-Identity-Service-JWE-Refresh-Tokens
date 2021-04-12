using Data.Repositores.Abstraction;
using Entities;
using Services.RefreshTokenDomain.Abstraction;
using System;
using System.Threading.Tasks;

namespace Services.RefreshTokenDomain.Implementation
{
    public class GetRefreshTokenService : IGetRefreshTokenService
    {
        private readonly IRefreshTokenRepository refreshTokenRepository;

        public GetRefreshTokenService(IRefreshTokenRepository refreshTokenRepository)
        {
            this.refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<RefreshToken> GetByRefreshToken(string refreshToken)
        {
            var result = await refreshTokenRepository.GetBytoken(new Guid(refreshToken));
            return result;
        }
    }
}
