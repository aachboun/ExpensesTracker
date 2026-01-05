using ExpensesTracker.Data;
using ExpensesTracker.Models;
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
        public async Task<Expense> GetById(int id ,string UserId)
        {
            return await _context.Expenses
                .FirstOrDefaultAsync(e => e.Id == id &&  e.UserId == UserId);

        }
        public async Task AddAsync(Expense expense)
        {
            await _context.Expenses.AddAsync(expense);

        }
        public void DeleteAsync(Expense expense) 
        {
             _context.Expenses.Remove(expense);
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
