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

        public async Task<Budget> GetGlobaleByIdAsync(int id , string UserId)
        {
           return await _context.Budgets.
                FirstOrDefaultAsync(b=>b.Id == id && b.UserId==UserId);

        }
        public async Task<Budget> GetGlobaleByIdAndPeriodeAsync(int id ,string UserId, DateTime from , DateTime To)
        {
            return await _context.Budgets.
                Where(b => b.Id == id && b.UserId == UserId && b.EffectiveFrom == from && b.EffectiveTo == To)
                .FirstOrDefaultAsync();
        }
        public async Task AddAsync(Budget budget)
        {
            await _context.Budgets.AddAsync(budget);
        }
        public void  Delete( Budget budget)
        {
             _context.Budgets.Remove(budget);
        }
        public async Task<Budget> GetByCategory(int CategoryId, string UserId)
        {
            return await _context.Budgets
                .FirstOrDefaultAsync(b=>b.CategoryId == CategoryId && b.UserId==UserId);
        }
        public async Task<Budget> GetByCatAndPeriode(int CategoryId, string UserId, DateTime from , DateTime To)
        {
            return await _context.Budgets
                .Where(
                b => b.CategoryId == CategoryId && b.UserId == UserId
                && b.EffectiveFrom == from && b.EffectiveTo == To)
                .FirstOrDefaultAsync();
                
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}


