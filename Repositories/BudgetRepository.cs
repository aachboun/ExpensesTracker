using ExpensesTracker.Data;
using ExpensesTracker.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using ExpensesTracker.Models;


namespace ExpensesTracker.Repositories
{
    public class BudgetRepository: IBudgetRepository
    {
            private readonly ApplicationDbContext _context;
            public BudgetRepository(ApplicationDbContext context)
            {
                _context = context;

            }

        public async Task<Budget> GetByIdAsync(int id , string UserId)
        {
           return await _context.Budgets.
                FirstOrDefaultAsync(b=>b.Id == id && b.UserId==UserId);

        }
        public async Task AddAsync(Budget budget)
        {
            await _context.Budgets.AddAsync(budget);
        }
        public void  Delete( Budget budget)
        {
             _context.Budgets.Remove(budget);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}


