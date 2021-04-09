using Entities;
using System.Threading.Tasks;

namespace Services.JWTServices.Abstraction
{
    public interface IJwtService
    {
        Task<string> GenerateAsync(ApplicationUser user);
    }
}