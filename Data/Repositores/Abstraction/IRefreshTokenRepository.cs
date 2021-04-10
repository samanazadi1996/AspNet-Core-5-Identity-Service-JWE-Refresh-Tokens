using Entities;
using System.Threading.Tasks;

namespace Data.Repositores.Abstraction
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken> GetById(int id);
        void Save();
        Task SaveAsync();
    }
}