﻿using System.Collections.Generic;
using System.Linq;

namespace Presentation.WebUI.Infrastructure.Authentication
{
    public class AuthenticatedUser
    {
        public bool IsAuthenticated { get; set; }
        public string Name { get; set; }
        public List<string> Roles { get; set; }

        public bool IsInRole(string role)
        {
            return Roles is not null && Roles.Any(p => p.Equals(role));
        }
    }
}