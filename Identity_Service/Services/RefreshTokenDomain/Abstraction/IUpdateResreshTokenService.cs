using System;
using System.Threading.Tasks;

namespace Services.RefreshTokenDomain.Abstraction
{
    public interface IUpdateResreshTokenService
    {
        Task<Guid> Update(Guid token, string ipAddress);
    }
}