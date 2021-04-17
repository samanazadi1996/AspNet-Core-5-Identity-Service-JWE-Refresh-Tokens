using System;

namespace Presentation.Models
{
    public class TokensDTO
    {
        public string token { get; set; }
        public Guid refreshToken { get; set; }
    }
}
