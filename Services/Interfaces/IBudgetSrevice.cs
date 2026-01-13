using ExpensesTracker.DTOs.Budget;

namespace ExpensesTracker.Services.Interfaces
{
    public interface IBudgetService
    {

        Task <ReadBudgetDto> GetByIdAsync(int? CategoryId, string UserId);
        Task<int> CreateAsync (CreateBudgetDto dto , string UserId);
        Task Delete(int? CategoryId, string UserId);
        Task UpdateBudgetAsync(int? @intCategoryId, UpdateBudgetDto dto , string UserId);
    }
}
