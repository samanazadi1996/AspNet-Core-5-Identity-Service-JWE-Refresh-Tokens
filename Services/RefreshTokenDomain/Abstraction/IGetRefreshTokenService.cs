using Entities;
using System.Threading.Tasks;

namespace Services.RefreshTokenDomain.Abstraction
{
    public interface IGetRefreshTokenService
    {
        Task<RefreshToken> GetByRefreshToken(string refreshToken);
    }
}
