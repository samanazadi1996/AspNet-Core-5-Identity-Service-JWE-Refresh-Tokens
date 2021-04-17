using System.Security.Claims;

namespace Services.JWTDomain.Abstraction
{
    public interface IGetClaimsByTokenService
    {
        ClaimsPrincipal Get(string token);
    }
}