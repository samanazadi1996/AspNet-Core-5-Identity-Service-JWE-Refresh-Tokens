using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.ReCaptchaDomain.Abstraction;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Threading.Tasks;

namespace Presentation.Mvc.Controllers
{
    public class ReCaptchaController : Controller
    {
        private readonly IGetImageReCaptchaService getImageReCaptchaService;

        public ReCaptchaController(IGetImageReCaptchaService getImageReCaptchaService)
        {
            this.getImageReCaptchaService = getImageReCaptchaService;
        }
        public void Captcha(long id)
        {
            string number = new Random().Next(1000, 9999).ToString();
            ViewBag.Number = number;
            HttpContext.Session.SetString("Captcha" + id, number);
        }

        public async Task<FileContentResult> ReCaptcha(long id)
        {
            Captcha(id);

            var session = HttpContext.Session.GetString("Captcha" + id);

            if (!string.IsNullOrEmpty(session))
            {
                var result = await getImageReCaptchaService.Get(session);
                return File(result, "image/jpeg");
            }
            return null;
        }
    }
}
