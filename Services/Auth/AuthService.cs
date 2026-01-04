using ExpensesTracker.DTOs.Auth;
using ExpensesTracker.Models;
using ExpensesTracker.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace ExpensesTracker.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtService _jwtService;

        public AuthService(
            UserManager<ApplicationUser> userManager,
            IJwtService jwtService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
        }

        public async Task RegisterAsync(RegisterDto dto)
        {
            var user = new ApplicationUser
            {
                UserName = dto.Email,
                Email = dto.Email
            };

            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                throw new ApplicationException(string.Join(" | ", errors));
            }
        }

        public async Task<string> LoginAsync(LoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);

            if (user == null)
                throw new UnauthorizedAccessException("Invalid credentials");

            var passwordValid = await _userManager.CheckPasswordAsync(user, dto.Password);

            if (!passwordValid)
                throw new UnauthorizedAccessException("Invalid credentials");

            return _jwtService.GenerateToken(user);
        }
    }
}

