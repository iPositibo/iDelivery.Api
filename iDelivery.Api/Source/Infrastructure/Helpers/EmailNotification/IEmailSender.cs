using System.Threading.Tasks;

namespace iDelivery.Api.Source.Infrastructure.Helpers.EmailNotification
{
    public interface IEmailSender
    {
        Task<bool> SendEmailAsync(EmailMessage message);
    }
}
