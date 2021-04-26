using System.Threading.Tasks;

namespace Services.MailDomain.Abstraction
{
    public interface ISendEmailService
    {
        Task SendAsync(string to, string body);
    }
}