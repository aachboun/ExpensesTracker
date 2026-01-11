using ExpensesTracker.DTOs.Budget;

namespace ExpensesTracker.Services.Interfaces
{
    public interface IBudgetService
    {

        Task <ReadBudgetDto> GetByIdAsync(int id, string UserId);
        Task<int> CreateAsync (CreateBudgetDto dto , string UserId);
        Task Delete(int id, string UserId);
        Task UpdateBudgetAsync(int id,UpdateBudgetDto dto , string UserId);
    }
}
