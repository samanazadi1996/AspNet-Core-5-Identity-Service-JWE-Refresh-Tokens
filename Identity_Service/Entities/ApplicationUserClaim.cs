using Microsoft.AspNetCore.Identity;

namespace Entities
{
    public class ApplicationUserClaim : IdentityUserClaim<string>
    {
        public ApplicationUser User { get; set; }

    }
}
