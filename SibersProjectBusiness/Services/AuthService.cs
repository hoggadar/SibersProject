using SibersProjectBusiness.Interfaces;

namespace SibersProjectBusiness.Services
{
    public class AuthService : IAuthService
    {
        public bool VerifyPassword(string password, string passwordHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, passwordHash);
        }

        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
    }
}
