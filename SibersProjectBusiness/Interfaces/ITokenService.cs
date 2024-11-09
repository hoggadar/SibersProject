using SibersProjectDataAccess.Entities;
using System.Security.Claims;

namespace SibersProjectBusiness.Interfaces
{
    public interface ITokenService
    {
        string GenerateAccessToken(IEnumerable<Claim> claims);
        ClaimsPrincipal GetPrincipalFromExpiredAccessToken(string token);
        Task<UserEntity?> GetUserFromExpiredAccessToken(string token);
    }
}
