using System.Net;
using System.Net.Mail;

namespace TaskScheduler
{
    public class MessageSender
    {    
        static MailAddress fromAddress = new MailAddress("dinislam.turmakhan@gmail.com", "do not reply");
        const string fromPassword = "123Islam";

        SmtpClient smtp = new SmtpClient
        {
            Host = "smtp.gmail.com",
            Port = 587,
            EnableSsl = true,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
        };

        public void SendMessage(string subject, string message, string toAddress)
        {
            using (var newMessage = new MailMessage(fromAddress.ToString(), toAddress )
            {
                Subject = subject,
                Body = message
            })
            {
                smtp.Send(newMessage);
            }
        }
    }
}
