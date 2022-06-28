using ErpDashboard.Application.Requests.Mail;

namespace ErpDashboard.Application.Interfaces.Services
{
    public interface IMailService
    {
        Task SendAsync(MailRequest request);
    }
}