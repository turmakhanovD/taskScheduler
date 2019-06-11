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

        public void SendMessage(object obj)
        {
            MessageAttributes message = obj as MessageAttributes;
            using (var newMessage = new MailMessage(fromAddress.ToString(), message.To )
            {
                Subject = message.Subject,
                Body = message.Message
            })
            {
                smtp.Send(newMessage);
            }
        }
    }
}
