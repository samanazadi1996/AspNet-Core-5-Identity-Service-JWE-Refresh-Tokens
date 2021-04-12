using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public class ApplicationUser : IdentityUser
    {
        public RefreshToken RefreshToken { get; set; }

        [ForeignKey("RefreshToken")]
        public int? RefreshTokenId { get; set; }
    }
}
