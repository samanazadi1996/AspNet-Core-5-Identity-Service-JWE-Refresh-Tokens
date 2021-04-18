using System.Collections.Generic;

namespace Presentation.WebUI.Models
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
