using Services.ReCaptchaDomain.Abstraction;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Threading.Tasks;

namespace Services.ReCaptchaDomain.Implementation
{
    public class GetImageReCaptchaService : IGetImageReCaptchaService
    {
        public Task<byte[]> Get(string number)
        {
            Bitmap photo = new Bitmap(100, 75);
            Graphics pointer = Graphics.FromImage(photo);
            HatchBrush backBrush = new HatchBrush(HatchStyle.Cross, Color.Silver, Color.White);

            pointer.FillRectangle(backBrush, 0, 0, 100, 75);
            pointer.RotateTransform(new Random().Next(-15, 15));
            pointer.DrawString(number, new Font("Comic Sans MS", 24, FontStyle.Bold), Brushes.Silver, new PointF(10, 20));
            pointer.Save();

            MemoryStream photoStream = new MemoryStream();
            photo.Save(photoStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            return Task.FromResult(photoStream.ToArray());
        }
    }
}
