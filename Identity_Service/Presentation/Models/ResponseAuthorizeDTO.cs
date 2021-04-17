using System;
using System.Collections.Generic;

namespace Presentation.Models
{
    public class ResponseAuthorizeDTO
    {
        public string name { get; set; }
        public IEnumerable<string> roles { get; set; }
        public string userId { get; set; }
    }
}
