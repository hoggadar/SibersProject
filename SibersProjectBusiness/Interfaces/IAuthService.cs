namespace SibersProjectBusiness.Interfaces
{
    public interface IAuthService
    {
        bool VerifyPassword(string password, string passwordHash);
        string HashPassword(string password);
    }
}
