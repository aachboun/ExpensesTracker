using ExpensesTracker.Models;

namespace ExpensesTracker.Services.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(ApplicationUser user);
    }
}
