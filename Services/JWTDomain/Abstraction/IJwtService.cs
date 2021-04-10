using Entities;
using System.Threading.Tasks;

namespace Services.JWTDomain.Abstraction
{
    public interface IJwtService
    {
        Task<string> GenerateAsync(ApplicationUser user);
    }
}