using System.ComponentModel.DataAnnotations;

namespace Presentation.Api.Models.ResetPassword
{
    public class PasswordDTO
    {
        [Required(ErrorMessage = "رمز عبور قبلی ضروری است")]
        [DataType(DataType.Password)]
        [Display(Name = "رمز عبور فعلی")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "*")]
        [StringLength(100, ErrorMessage = "{0} باید حداقل {2} حرف طولانی داشته باشد.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "رمز عبور جدید")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "*")]
        [DataType(DataType.Password)]
        [Display(Name = "تکرار رمز عبور جدید")]
        [Compare("NewPassword", ErrorMessage = "رمز عبور جدید و رمز عبور تأییدکننده مطابقت ندارد")]
        public string ConfirmPassword { get; set; }
    }


}
