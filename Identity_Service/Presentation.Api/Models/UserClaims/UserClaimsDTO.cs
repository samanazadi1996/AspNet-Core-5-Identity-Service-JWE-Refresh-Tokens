using Presentation.Api.Models.Common;
using System.Collections.Generic;

namespace Presentation.Api.Models.UserClaims
{
    public class UserClaimsDTO
    {
        public string UserName { get; set; }

        public List<ClaimDTO> Claims { get; set; }
    }
}
