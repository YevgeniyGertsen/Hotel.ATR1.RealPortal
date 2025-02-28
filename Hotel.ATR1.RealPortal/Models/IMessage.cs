namespace Hotel.ATR1.RealPortal.Models
{
    public interface IMessage
    {
        public bool SendMessage(string to, string textMessage);
    }
}
