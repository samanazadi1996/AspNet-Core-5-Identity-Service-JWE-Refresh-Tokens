using Microsoft.AspNetCore.Identity;

namespace Entities
{
    public class ApplicationUserRole : IdentityUserRole<string>
    {
        public ApplicationUser User { get; set; }

        public ApplicationRole Role { get; set; }
    }

}
