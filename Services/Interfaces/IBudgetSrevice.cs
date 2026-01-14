using ExpensesTracker.DTOs.Budget;
using ExpensesTracker.Models;

namespace ExpensesTracker.Services.Interfaces
{
    public interface IBudgetService
    {

        Task <ReadBudgetDto> GetByIdAsync(int? CategoryId, string UserId);
        Task<ReadBudgetDto> GetByPeriodeAsync(int? CategoryId, string UserId, DateOnly from, DateOnly To);
        Task<int> CreateAsync (CreateBudgetDto dto , string UserId);
        Task Delete(int? CategoryId, string UserId);
        Task UpdateBudgetAsync(int? @intCategoryId, UpdateBudgetDto dto , string UserId);
    }
}
