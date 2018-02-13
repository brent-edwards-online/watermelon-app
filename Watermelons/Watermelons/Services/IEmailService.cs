using System.Threading.Tasks;


namespace Watermelons.Services
{
    public interface IEmailService
    {
        bool EmailMessage(string toAddress, string subject, string message);
    }
} 
