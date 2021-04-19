using Presentation.Models.Common;

namespace Presentation.Models.UserClaims
{
    public class RoleClaimDTO
    {
        public string RoleName { get; set; }

        public ClaimDTO Claim { get; set; }
    }
}
