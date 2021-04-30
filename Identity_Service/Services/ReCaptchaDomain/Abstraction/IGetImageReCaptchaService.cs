using System.Threading.Tasks;

namespace Services.ReCaptchaDomain.Abstraction
{
    public interface IGetImageReCaptchaService
    {
        Task<byte[]> Get(string number);
    }
}