using Hotel.ATR1.RealPortal.Models;

namespace Hotel.ATR1.RealPortal.Services
{
    public interface IAuthService
    {
        Task<User> ValidateUser(string username, string password);
    }
}
