using System;

namespace Presentation.Models
{
    public class ResponseAuthorizeDTO
    {
        public string name { get; set; }
        public string roles { get; set; }
        public NewTokensDTO newData { get; set; }
    }
    public class NewTokensDTO
    {
        public string token { get; set; }
        public string refreshToken { get; set; }
    }
}
