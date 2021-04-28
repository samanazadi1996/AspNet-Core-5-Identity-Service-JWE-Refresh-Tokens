using System.Collections.Generic;
using System.Linq;

namespace Identity.Client
{
    public class AuthenticatedUser
    {
        public bool IsAuthenticated { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Token { get; set; }
        public List<string> Roles { get; set; }

        public bool IsInRole(string role)
        {
            return Roles is not null && Roles.Any(p => p.Equals(role));
        }
    }
}
