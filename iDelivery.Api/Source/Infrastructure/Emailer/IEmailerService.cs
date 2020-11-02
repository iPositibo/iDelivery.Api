using System.Threading.Tasks;

namespace iDelivery.Api.Source.Infrastructure.Emailer
{
    public interface IEmailerService
    {
        Task Send(string message, string recipient);
    }
}
