using System.ComponentModel.DataAnnotations;

namespace Presentation.Models.ResetPassword
{
    public class ForgotPasswordDTO
    {
        [Required]
        public string UserName { get; set; }

        public long sessionId { get; set; }

    }


}
