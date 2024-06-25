using System.Security.Claims;
using TechAssess.AuthService.Models;

namespace TechAssess.AuthService.Services
{
    public interface IJwtService
    {
        string GenerateAccessToken(User user);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
