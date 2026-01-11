using ExpensesTracker.Models;


namespace ExpensesTracker.Repositories.Interfaces
{
    public interface IBudgetRepository
    {
        Task<Budget> GetByIdAsync(int id,  string UserId);
        Task AddAsync(Budget budget);
        Task Delete(Budget budget);
        Task SaveCahngesAsync();

    }
}
