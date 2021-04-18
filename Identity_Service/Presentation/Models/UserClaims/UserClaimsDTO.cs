using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Presentation.Models.UserClaims
{
    public class UserClaimsDTO
    {
        public string userName { get; set; }

        public List<ClaimDTO> claims { get; set; }
    }

    public class ClaimDTO
    {
        public string type { get; set; }
        public string value { get; set; }
    }
}
