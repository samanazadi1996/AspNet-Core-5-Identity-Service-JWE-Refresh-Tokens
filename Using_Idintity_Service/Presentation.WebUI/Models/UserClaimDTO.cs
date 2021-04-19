using System.Collections.Generic;

namespace Presentation.WebUI.Models
{
    public class UserClaimDTO
    {
        public string UserName { get; set; }

        public ClaimDTO Claim { get; set; }
    }

}
