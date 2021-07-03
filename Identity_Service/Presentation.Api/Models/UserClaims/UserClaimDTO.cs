using Presentation.Api.Models.Common;

namespace Presentation.Api.Models.UserClaims
{
    public class UserClaimDTO
    {
        public string UserName { get; set; }

        public ClaimDTO Claim { get; set; }
    }
}
