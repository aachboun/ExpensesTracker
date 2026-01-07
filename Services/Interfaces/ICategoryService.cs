using ExpensesTracker.DTOs.Category;

namespace ExpensesTracker.Services.Interfaces
{
    public interface ICategoryService
    {

        Task<int> CreateAsync(CreateCategoryDto dto , string UserId);
        Task UpdateAsync( int id , UpdateCategoryDto dto , string UserId);
        Task DeleteAsync(int id , string UserId);
        Task<List<ReadCategoryDto>> GetAllAsync(string UserId);
        Task<ReadCategoryDto> GetByIdAsync(int id , string UserId);
    }
}
