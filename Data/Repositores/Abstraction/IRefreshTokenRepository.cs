using Entities;
using System;
using System.Threading.Tasks;

namespace Data.Repositores.Abstraction
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken> GetBytoken(Guid token);
        Task<RefreshToken> GetById(int id);
        void Save();
        Task SaveAsync();
    }
}