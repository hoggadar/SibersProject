using Microsoft.AspNetCore.Mvc;
using SibersProjectBusiness.DTOs.Auth;
using SibersProjectBusiness.DTOs.User;
using SibersProjectBusiness.Interfaces;
using SibersProjectDataAccess.Enums;
using System.Security.Claims;

namespace SibersProjectWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userServices;
        private readonly ITokenService _tokenService;
        private readonly IAuthService _authService;

        public AuthController(IUserService userServices, ITokenService tokenService, IAuthService authService)
        {
            _userServices = userServices;
            _tokenService = tokenService;
            _authService = authService;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> Signup([FromBody] SignupDto dto)
        {
            var user = await _userServices.GetByEmail(dto.Email);
            if (user != null) return BadRequest("User already exists");
            var hashedPassword = _authService.HashPassword(dto.Password);
            var userDto = new CreateUserDto
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Patronymic = dto.Patronymic,
                Email = dto.Email,
                Password = hashedPassword,
                Role = RoleEnum.Employee.ToString(),
            };
            var createdUser = await _userServices.Create(userDto);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, createdUser.Email),
                new Claim(ClaimTypes.Role, createdUser.Role.ToString())
            };
            string accessToken = _tokenService.GenerateAccessToken(claims);
            return Ok(new { accessToken });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var user = await _userServices.GetByEmail(dto.Email);
            if (user == null || !_authService.VerifyPassword(dto.Password, user.Password))
            {
                return Unauthorized("Incorrect email or password");
            }
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };
            string accessToken = _tokenService.GenerateAccessToken(claims);
            return Ok(new { accessToken });
        }
    }
}
