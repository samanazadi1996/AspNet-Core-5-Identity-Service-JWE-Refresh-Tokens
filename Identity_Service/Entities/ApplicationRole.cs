using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Entities
{
    public class ApplicationRole : IdentityRole<string>
    {
        public List<ApplicationUserRole> UserRoles { get; set; }
        public List<ApplicationRoleClaim> RoleClaims { get; set; }
    }

}
