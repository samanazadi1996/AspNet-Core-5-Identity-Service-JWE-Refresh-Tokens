using System.ComponentModel.DataAnnotations;

namespace Presentation.Api.Models.ResetPassword
{
    public class ForgotPasswordDTO
    {
        [Required]
        public string UserName { get; set; }
        public long sessionId { get; set; }
        public string txtCaptcha { get; set; }
        public string UCB { get; set; }
    }


}
