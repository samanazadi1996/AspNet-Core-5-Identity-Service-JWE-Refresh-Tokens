using System.ComponentModel.DataAnnotations;

namespace Presentation.Api.Models.ResetPassword
{
    public class ResetPasswordDTO
    {
        [Required]
        public string UserName { get; set; }

        public string Token { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "کلمه ی عبور با تکرار آن همخوانی ندارد")]
        public string ConfirmPassword { get; set; }

        public string UCB { get; set; }
    }

}
