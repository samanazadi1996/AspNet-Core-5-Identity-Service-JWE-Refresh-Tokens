using Entities;
using System;
using System.Threading.Tasks;

namespace Services.RefreshTokenDomain.Abstraction
{
    public interface IGenerateResreshTokenService
    {
        Task<Guid> Generate(ApplicationUser user, string ipAddress);
    }
}
