using System;

namespace Presentation.Models
{
    public class ResponseAuthorizeDTO
    {
        public string name { get; set; }
        public string roles { get; set; }
        public string userId { get; set; }
        public TokensDTO newData { get; set; }
    }
}
