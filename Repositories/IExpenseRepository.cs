using ExpensesTracker.Models;

namespace ExpensesTracker.Repositories
{
    public interface IExpenseRepository
    {

        Task<List<Expense>> GetExpenses(string UserId);
        Task<Expense> GetById(int id,string UserId);
        Task AddAsync(Expense expense);
        void DeleteAsync(Expense expense);
        Task SaveChangesAsync();
    }
}
