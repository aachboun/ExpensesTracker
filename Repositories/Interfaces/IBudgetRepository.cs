using ExpensesTracker.Models;


namespace ExpensesTracker.Repositories.Interfaces
{
    public interface IBudgetRepository
    {
        Task<Budget> GetCurrentBudgetAsync(int? CategoryId,  string UserId);
        Task<Budget> GetByPeriodeAsync(int? CategoryId , string UserId,DateOnly from, DateOnly To);
        Task AddAsync(Budget budget);
        void Delete(Budget budget);
        Task SaveChangesAsync();

    }
}
