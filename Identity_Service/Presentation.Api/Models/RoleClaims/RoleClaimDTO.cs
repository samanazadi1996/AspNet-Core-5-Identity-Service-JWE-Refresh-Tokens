using Presentation.Api.Models.Common;

namespace Presentation.Api.Models.UserClaims
{
    public class RoleClaimDTO
    {
        public string RoleName { get; set; }

        public ClaimDTO Claim { get; set; }
    }
}
