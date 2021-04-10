using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Models
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

        public string UrlCallBack { get; set; }
    }
}
