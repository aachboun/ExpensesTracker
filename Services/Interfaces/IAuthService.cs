using ExpensesTracker.DTOs.Auth;

namespace ExpensesTracker.Services.Interfaces
{
    public interface IAuthService
    {
        Task RegisterAsync(RegisterDto dto);
        Task<string> LoginAsync(LoginDto dto);
    }
}
