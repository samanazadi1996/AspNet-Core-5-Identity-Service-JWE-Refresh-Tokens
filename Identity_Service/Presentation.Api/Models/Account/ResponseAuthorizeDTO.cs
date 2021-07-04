using System.Collections.Generic;

namespace Presentation.Api.Models.Account
{
    public class ResponseAuthorizeDTO
    {
        public string name { get; set; }
        public IEnumerable<string> roles { get; set; }
        public string userId { get; set; }
    }
}
