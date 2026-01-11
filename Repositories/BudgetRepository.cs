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

        }
    
    }
}


