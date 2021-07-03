using System.ComponentModel.DataAnnotations;

namespace Presentation.Api.Models.ResetPassword
{
    public class ChangePasswordDTO
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string OldPassword { get; set; }

        [Required]
        public string NewPassword { get; set; }

        [Compare(nameof(NewPassword))]
        public string ConfirmPassword { get; set; }
    }
}
