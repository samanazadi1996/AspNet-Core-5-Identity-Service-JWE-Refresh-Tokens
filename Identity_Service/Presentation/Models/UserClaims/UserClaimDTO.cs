﻿using Presentation.Models.Common;

namespace Presentation.Models.UserClaims
{
    public class UserClaimDTO
    {
        public string UserName { get; set; }

        public ClaimDTO Claim { get; set; }
    }
}
