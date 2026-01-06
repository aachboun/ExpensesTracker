using ExpensesTracker.Data;
using ExpensesTracker.Repositories.Interfaces;
using ExpensesTracker.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ExpensesTracker.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<List<Category>> GetAllAsync(string UserId)
        {
            return await _context.Categories
                .AsNoTracking()
                .Where(c => c.UserId == UserId)
                .OrderByDescending(c => c.Name)
                .ToListAsync();

        }
        public async Task<Category> GetByIdAsync(int id, string UserId)
        {
            return await _context.Categories
                         .FirstOrDefaultAsync(c => c.Id == id && c.UserId == UserId);

        }
        public async Task AddAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
        }
        public void DeleteAsync(Category category) 
        {
             _context.Categories.Remove(category);
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
