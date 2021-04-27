using Microsoft.AspNetCore.Identity;

namespace Entities
{
    public class ApplicationRoleClaim : IdentityRoleClaim<string>
    {
        public ApplicationRole Role { get; set; }
    }

}
