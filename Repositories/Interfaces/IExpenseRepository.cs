using ExpensesTracker.Models;
using System.Globalization;

namespace ExpensesTracker.Repositories.Interfaces
{
    public interface IExpenseRepository
    {

        Task<List<Expense>> GetExpenses(string UserId);
        Task<Expense> GetById(int Id,string UserId);
        Task<List<Expense>> GetByPeriod(string UserId, DateOnly From , DateOnly To);
        Task<List<Expense>> GetByCatAndPeriod(int CategoryId, string UserId, DateOnly From, DateOnly To);
        Task AddAsync(Expense expense);
        void Delete(Expense expense);
        Task SaveChangesAsync();
    }
}
