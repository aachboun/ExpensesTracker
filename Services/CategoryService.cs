using AutoMapper;
using ExpensesTracker.Services.Interfaces;
using Microsoft.EntityFrameworkCore.Diagnostics;
using ExpensesTracker.DTOs.Category;
using ExpensesTracker.Models;
using ExpensesTracker.Repositories.Interfaces;

namespace ExpensesTracker.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<CategoryService> _logger;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper , ILogger<CategoryService> logger)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<int> CreateAsync(CreateCategoryDto dto, string UserId)
        {
            _logger.LogInformation("Creating Category | UserId:{UserId}", UserId);
            var category = _mapper.Map<Category>(dto);
            category.UserId = UserId;
            await _categoryRepository.AddAsync(category);
            await _categoryRepository.SaveChangesAsync();

            _logger.LogInformation("Category Created | id : {Id}, UserId: {UserId}", category.Id, UserId);

            return category.Id;
        }
        public async Task<List<ReadCategoryDto>> GetAllAsync(string UserId)
        {
            var categories = await _categoryRepository.GetAllAsync(UserId);
            return _mapper.Map<List<ReadCategoryDto>>(categories);    
            
        }
        public async Task<ReadCategoryDto> GetByIdAsync(int id, string UserId)
        {
            var category = await _categoryRepository.GetByIdAsync(id, UserId);
            if (category == null) 
            {
                _logger.LogWarning("Category Not Found | id :{Id}, UserId:{UserId}",id, UserId);
                throw new KeyNotFoundException("Category not found");
            }

            return _mapper.Map<ReadCategoryDto>(category);

        }
        public  async Task DeleteAsync(int id , string UserId)
        {
            var category = await _categoryRepository.GetByIdAsync(id, UserId);
            if(category == null)
            {
                _logger.LogWarning("Category Not Found| id : {Id}, UserId : {UserId}", id, UserId);
                throw new KeyNotFoundException("Category Not Found");
            }
             _categoryRepository.DeleteAsync(category);
            await _categoryRepository.SaveChangesAsync();
            _logger.LogInformation("Category Deleted| id : {Id} , UserId:{UserId}", id, UserId);
        }
        public async Task UpdateAsync( int id ,UpdateCategoryDto dto , string UserId)
        {
            var category = await _categoryRepository.GetByIdAsync(id, UserId);
            if (category == null)
            {
                _logger.LogWarning("Category Not Found| id : {Id}, UserId : {UserId}", id, UserId);
                throw new KeyNotFoundException("Category Not Found");
            }
            _mapper.Map(dto,category);
            await _categoryRepository.SaveChangesAsync();
            _logger.LogInformation("Category Updated | Id : {Id} |UserId: {UserId}", id, UserId);

        }
    }
}
