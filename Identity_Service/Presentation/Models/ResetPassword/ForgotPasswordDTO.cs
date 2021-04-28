﻿using System.ComponentModel.DataAnnotations;

namespace Presentation.Models.ResetPassword
{
    public class ForgotPasswordDTO
    {
        [Required]
        public string UserName { get; set; }

        public string txtCaptcha { get; set; }
        public string UCB { get; set; }
    }


}
