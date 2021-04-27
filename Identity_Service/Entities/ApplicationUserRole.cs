using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Entities
{
    public class ApplicationUserRole : IdentityUserRole<string>
    {
        public ApplicationUser User { get; set; }

        public ApplicationRole Role { get; set; }
        public List<ApplicationRoleClaim> RoleClaims { get; set; }

    }

}
