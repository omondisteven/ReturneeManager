using ReturneeManager.Application.Requests.Mail;
using System.Threading.Tasks;

namespace ReturneeManager.Application.Interfaces.Services
{
    public interface IMailService
    {
        Task SendAsync(MailRequest request);
    }
}