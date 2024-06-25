using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechAssess.AuthService.Dto;
using TechAssess.AuthService.Services;

namespace TechAssess.AuthService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponse>> Login(AuthRequest request)
        {
            try
            {
                var response = await _authService.Login(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Unauthorized(new { ex.Message });
            }

        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<AuthResponse>> RefreshToken(RefreshTokenRequest request)
        {
            try
            {
                var response = await _authService.RefreshToken(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Unauthorized(new { ex.Message });
            }
        }
    }
}
