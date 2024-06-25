using TechAssess.AuthService.Dto;

namespace TechAssess.AuthService.Services
{
    public interface IAuthService
    {
        Task<AuthResponse> Login(AuthRequest request);
        Task<AuthResponse> RefreshToken(RefreshTokenRequest request);
    }
}
