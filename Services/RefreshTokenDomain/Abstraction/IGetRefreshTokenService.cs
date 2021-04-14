using Entities;
using System;
using System.Threading.Tasks;

namespace Services.RefreshTokenDomain.Abstraction
{
    public interface IGetRefreshTokenService
    {
        Task<RefreshToken> GetByRefreshToken(Guid refreshToken);
    }
}
