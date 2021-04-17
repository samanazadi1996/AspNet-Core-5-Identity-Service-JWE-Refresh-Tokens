using Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Repositores.Abstraction
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken> GetBytoken(Guid token);
        Task<RefreshToken> GetById(int id);
        Task Create(RefreshToken refreshToken);
        void Save();
        Task SaveAsync();
    }
}