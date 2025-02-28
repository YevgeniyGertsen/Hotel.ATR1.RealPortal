using Hotel.ATR1.RealPortal.Models;

namespace Hotel.ATR1.RealPortal.Services
{
    public class AuthService : IAuthService
    {
        public async Task<User> ValidateUser(string username, string password)
        {
            User user = new User()
            {
                Login = username,
                Password = password,
                FirstName = "Yevgeniy",
                LastName = "Gertsen"
            };

            return user;
        }
    }
}
