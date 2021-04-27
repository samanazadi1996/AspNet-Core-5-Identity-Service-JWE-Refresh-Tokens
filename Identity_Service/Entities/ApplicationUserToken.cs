using Microsoft.AspNetCore.Identity;

namespace Entities
{
    public class ApplicationUserToken : IdentityUserToken<string>
    {
        public ApplicationUser User { get; set; }

    }
}
