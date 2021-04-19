using System;

namespace Presentation.Models.Account
{
    public class TokensDTO
    {
        public string token { get; set; }
        public Guid refreshToken { get; set; }
    }
}
