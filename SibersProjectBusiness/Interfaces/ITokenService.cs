using SibersProjectBusiness.DTOs.User;
using System.Security.Claims;

namespace SibersProjectBusiness.Interfaces
{
    public interface ITokenService
    {
        string GenerateAccessToken(IEnumerable<Claim> claims);
        ClaimsPrincipal GetPrincipalFromExpiredAccessToken(string token);
        Task<UserDto?> GetUserFromExpiredAccessToken(string token);
    }
}
