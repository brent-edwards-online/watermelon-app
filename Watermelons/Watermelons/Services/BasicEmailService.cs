using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Watermelons.Services
{
    public class BasicEmailService : IEmailService
    {
        readonly MailAddress FROMADDRESS = new MailAddress("li.watermellon.demo@gmail.com", "LI Watermelon Demo");
        //readonly string FROMPASSWORD = "B30L3bunny18";
        readonly string FROMPASSWORD = "lsmtnjbiqsyffihi";

        public bool EmailMessage(string toAddress, string subject, string body)
        {

            try
            {
                var smtp = new SmtpClient
                {
                    UseDefaultCredentials = false,
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new NetworkCredential(FROMADDRESS.Address, FROMPASSWORD)
                };
                using (var message = new MailMessage(FROMADDRESS.Address, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(message);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }
    }
}