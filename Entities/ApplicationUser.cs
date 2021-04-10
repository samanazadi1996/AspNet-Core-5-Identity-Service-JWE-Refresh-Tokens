using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Entities
{
    public class ApplicationUser : IdentityUser
    {
        private RefreshToken refreshToken;

        public RefreshToken GetRefreshToken()
        {
            return refreshToken;
        }

        public void SetRefreshToken(RefreshToken value)
        {
            refreshToken = value;
        }

        public int? RefreshTokenId { get; set; }
    }
}
