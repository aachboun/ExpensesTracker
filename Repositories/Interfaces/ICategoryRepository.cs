using ExpensesTracker.DTOs.Category;
using ExpensesTracker.Models;

namespace ExpensesTracker.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllAsync(string UserId);
        Task<Category> GetByIdAsync(int id, string UserId);
        

        Task AddAsync(Category category);
        void DeleteAsync(Category category);
        Task SaveChangesAsync();

    }
}
