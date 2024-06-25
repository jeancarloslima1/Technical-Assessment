using Microsoft.EntityFrameworkCore;
using TechAssess.AuthService.Data;
using TechAssess.AuthService.Dto;

namespace TechAssess.AuthService.Services
{
    public class AuthService : IAuthService
    {
        private readonly DataContext _context;
        private readonly IJwtService _jwtService;

        public AuthService(DataContext context, IJwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }
        public async Task<AuthResponse> Login(AuthRequest request)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Username == request.Username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                throw new Exception("Invalid credentials");

            var accessToken = _jwtService.GenerateAccessToken(user);
            var refreshToken = _jwtService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

            await _context.SaveChangesAsync();
            return new()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };

        }

        public async Task<AuthResponse> RefreshToken(RefreshTokenRequest request)
        {
            var principal = _jwtService.GetPrincipalFromExpiredToken(request.AccessToken);
            if (principal.Identity == null) throw new Exception("Invalid access token");
            var username = principal.Identity.Name;

            var user = await _context.Users.SingleOrDefaultAsync(u => u.Username == username);

            if (user == null || user.RefreshToken != request.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
                throw new Exception("Invalid refresh token");

            var newAccessToken = _jwtService.GenerateAccessToken(user);
            var newRefreshToken = _jwtService.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            await _context.SaveChangesAsync();

            return new()
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            };
        }
    }
}
