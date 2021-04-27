using Microsoft.AspNetCore.Identity;

namespace Entities
{
    public class ApplicationUserLogin : IdentityUserLogin<string>
    {
        public ApplicationUser User { get; set; }

    }
}
