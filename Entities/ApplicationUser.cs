using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Entities
{
    public class ApplicationUser : IdentityUser
    {
        public RefreshToken? RefreshTokens { get; set; }
    }
}
