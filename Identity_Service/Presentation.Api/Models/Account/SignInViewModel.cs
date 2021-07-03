using System.ComponentModel.DataAnnotations;

namespace Presentation.Api.Models.Account
{

    public class SignInViewModel
    {
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "پرکردن فیلد {0} اجباری است")]
        public string UserName { get; set; }

        [Display(Name = "گذرواژه")]
        [Required(ErrorMessage = "پرکردن فیلد {0} اجباری است")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool ReMemberMe { get; set; }
        public string UCB { get; set; }
    }
}
