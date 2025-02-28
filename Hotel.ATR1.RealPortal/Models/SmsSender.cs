using Twilio.Types;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Hotel.ATR1.RealPortal.Models
{
    public class SmsSender : IMessage
    {
        public bool SendMessage(string to, string textMessage)
        {
            const string accountSid = "AC96a4253c3d08fa134394648150af0679";
            const string authToken = "874374e4c5d3ce0ae4e9ef93068703ed";

            TwilioClient.Init(accountSid, authToken);

            try
            {
                var message = MessageResource.Create(
                                body: textMessage,
                                from: new PhoneNumber("+18126136069"), // Тестовый номер Twilio
                                to: new PhoneNumber(to) // Кому отправляем
                            );
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
