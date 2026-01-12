using ExpensesTracker.Models;


namespace ExpensesTracker.Repositories.Interfaces
{
    public interface IBudgetRepository
    {
        Task<Budget> GetGlobaleByIdAsync(int id,  string UserId);
        Task<Budget> GetGlobaleByIdAndPeriodeAsync(int id , string UserId,DateTime from, DateTime To);
        Task <Budget> GetByCategory(int CategoryId, string UserId);
        Task<Budget> GetByCatAndPeriode(int CategoryId, string UserId, DateTime from, DateTime To);
        Task AddAsync(Budget budget);
        void Delete(Budget budget);
        Task SaveChangesAsync();

    }
}
