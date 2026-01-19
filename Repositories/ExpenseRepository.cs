using ExpensesTracker.Data;
using ExpensesTracker.Models;
using ExpensesTracker.Repositories.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace ExpensesTracker.Repositories
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly ApplicationDbContext _context;
        public ExpenseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async  Task<List<Expense>> GetExpenses(string UserId)
        {
            return await _context.Expenses
                   .AsNoTracking()
                   .Where(e => e.UserId == UserId)
                   .OrderByDescending(t=>t.Date)
                   .ToListAsync();    
        }
        public async Task<Expense> GetById(int Id ,string UserId)
        {
            return await _context.Expenses
                .FirstOrDefaultAsync(e => e.Id== Id &&  e.UserId == UserId);

        }
        public async Task<List<Expense>> GetByPeriod(string UserId, DateOnly From, DateOnly To)
        {
            return await _context.Expenses
                    .AsNoTracking()
                    .Where(e=>
                         e.UserId == UserId &&
                         e.Date >= From &&
                         e.Date <= To
                     )
                    .ToListAsync();
        }
        public async Task<List<Expense>> GetByCatAndPeriod(int CategoryId, string UserId, DateOnly From, DateOnly To)
        {
            return await _context.Expenses
                .AsNoTracking()
                .Where(e=>
                e.CategoryId== CategoryId &&
                e.UserId == UserId &&
                e.Date >= From &&
                e.Date <= To)
                .ToListAsync();
                
        }
        public async Task AddAsync(Expense expense)
        {
            await _context.Expenses.AddAsync(expense);

        }
        public void Delete(Expense expense) 
        {
             _context.Expenses.Remove(expense);
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
