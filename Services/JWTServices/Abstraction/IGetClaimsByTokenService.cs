using System.Security.Claims;

namespace Services.JWTServices.Abstraction
{
    public interface IGetClaimsByTokenService
    {
        ClaimsPrincipal Get(string token);
    }
}