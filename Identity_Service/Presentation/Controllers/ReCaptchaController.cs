using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;

namespace Presentation.Controllers
{
    public class ReCaptchaController : Controller
    {
        public void Captcha()
        {
            string number = new Random().Next(1000, 9999).ToString();
            ViewBag.Number = number;
            HttpContext.Session.SetString("Captcha", number);
        }

        public FileContentResult ReCaptcha()
        {
            Captcha();

            var session = HttpContext.Session.GetString("Captcha");

            if (!string.IsNullOrEmpty(session))
            {
                var number = session;
                Bitmap photo = new Bitmap(100, 75);
                Graphics pointer = Graphics.FromImage(photo);
                HatchBrush backBrush = new HatchBrush(HatchStyle.Cross, Color.Silver, Color.White);

                pointer.FillRectangle(backBrush, 0, 0, 100, 75);
                pointer.RotateTransform(new Random().Next(-15, 15));
                pointer.DrawString(number, new Font("arial", 24), Brushes.Silver, new PointF(10, 20));
                pointer.Save();

                MemoryStream photoStream = new MemoryStream();
                photo.Save(photoStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                var bitmapBytes = File(photoStream.ToArray(), "image/jpeg");//Convert bitmap into a byte array

                return bitmapBytes;
            }
            return null;
        }
    }

}
