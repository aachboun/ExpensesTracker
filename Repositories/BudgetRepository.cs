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

        public async Task<Budget> GetCurrentBudgetAsync(int? CategoryId , string UserId)
        {
           return await _context.Budgets.
                FirstOrDefaultAsync(b=>
                b.CategoryId == CategoryId && 
                b.UserId==UserId &&
                b.EffectiveTo == null);

        }
        public async Task<Budget> GetByPeriodeAsync(int? CategoryId ,string UserId, DateOnly from , DateOnly To)
        {
            return await _context.Budgets.
                Where(b => b.CategoryId == CategoryId && 
                b.UserId == UserId &&
                b.EffectiveFrom <= To && 
                b.EffectiveTo ==null ||b.EffectiveTo >= from)
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
     
      

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}


