using System.Net.Mail;
using System.Net;
using Twilio.TwiML.Messaging;

namespace Hotel.ATR1.RealPortal.Models
{
    public class EmailSender : IMessage
    {
        public bool SendMessage(string to, string textMessage)
        {
            #region config
            var fromAddress = new MailAddress("gersen.e.a@gmail.com", "From Name");
            var toAddress = new MailAddress(to, "To Name");
            const string fromPassword = "hppc dzmw iull lxvk";
            #endregion

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };

            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = "",
                Body = textMessage
            })
            {
                try
                {
                    smtp.Send(message);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}
