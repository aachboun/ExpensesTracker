using AutoMapper;
using ExpensesTracker.DTOs.Budget;
using ExpensesTracker.Repositories.Interfaces;
using ExpensesTracker.Services.Interfaces;
using ExpensesTracker.Models;


namespace ExpensesTracker.Services
{
    public class BudgetService: IBudgetService
    {
        private readonly IBudgetRepository _budgetRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<BudgetService> _logger;


        public BudgetService(IBudgetRepository budgetRepository, IMapper mapper, ILogger<BudgetService> logger)
        {
            _budgetRepository = budgetRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task <ReadBudgetDto> GetByPeriodeAsync(int? CategoryId,string UserId, DateOnly from , DateOnly To)
        {
            var budget = await _budgetRepository.GetByPeriodeAsync(CategoryId, UserId,from,To);
            if (budget == null)
            {
                _logger.LogWarning("Budgte Not Found | CategoryId:{id}, UserId:{UserId}", CategoryId, UserId);
                throw new KeyNotFoundException("Budget Not Found ");

            }
            return _mapper.Map<ReadBudgetDto>(budget);
        }
        public async Task<ReadBudgetDto> GetByIdAsync(int? CategoryId , string UserId)
        {
            var budget = await _budgetRepository.GetCurrentBudgetAsync(CategoryId, UserId);
            if (budget == null) 
            {
              _logger.LogWarning("Budgte Not Found | CategoryId:{id}, UserId:{UserId}",CategoryId, UserId);
                throw new KeyNotFoundException("Budget Not Found ");
                
            } 
          return _mapper.Map<ReadBudgetDto>(budget);
        }

        public async Task<int> CreateAsync(CreateBudgetDto dto, string UserId, int? CategoryId)
        {
            _logger.LogInformation("Creating  budget userid: {UserId}", UserId);

            var existing = await _budgetRepository
                 .GetCurrentBudgetAsync(CategoryId, UserId);

            if (existing != null)
            {
                _logger.LogWarning(" Delete Failed | Budget Not found | id :{Id}, UserId : {UserId}", existing.Id, UserId);
                throw new InvalidOperationException("An active budget already exists");
            }

            var budget = _mapper.Map<Budget>(dto);
            budget.UserId = UserId;
            budget.CategoryId = CategoryId;
            budget.EffectiveFrom = DateOnly.FromDateTime(DateTime.UtcNow);
            await _budgetRepository.AddAsync(budget);
            await _budgetRepository.SaveChangesAsync();

            _logger.LogInformation("Budget Created Successfully | id: {Budget.Id}, UserId:{UserId}", budget.Id, UserId);

            return budget.Id;
        }
        public async Task Delete(int? CategoryId, string UserId)
        {
            var budget = await _budgetRepository.GetCurrentBudgetAsync(CategoryId, UserId);  
            if(budget == null)
            {
                _logger.LogWarning(" Delete Failed | Budget Not found | id :{Id}, UserId : {UserId}", CategoryId, UserId);
                throw new KeyNotFoundException("Budget Not Found ");
            }
            _budgetRepository.Delete(budget);
            await _budgetRepository.SaveChangesAsync(); 

            _logger.LogInformation(" Budget deleted| id : {Id}, UserId:{UserId}",CategoryId, UserId);
        }

        public async Task UpdateBudgetAsync(int? categoryId, UpdateBudgetDto dto, string userId)
        {
            var currentBudget = await _budgetRepository
                .GetCurrentBudgetAsync(categoryId, userId);

            if (currentBudget == null)
            {
                _logger.LogWarning(
                    "Budget not found | CategoryId:{CategoryId}, UserId:{UserId}",
                    categoryId, userId);

                throw new KeyNotFoundException("Budget not found");
            }

            // 1️⃣ Fermer l’ancien budget
            currentBudget.EffectiveTo = DateOnly.FromDateTime(DateTime.UtcNow);

            // 2️⃣ Créer le nouveau budget
            var newBudget = _mapper.Map<Budget>(currentBudget);
            newBudget.Id = 0;
            newBudget.EffectiveFrom = DateOnly.FromDateTime(DateTime.UtcNow);
            newBudget.EffectiveTo = null;
            _mapper.Map(dto, newBudget);
            await _budgetRepository.AddAsync(newBudget);
            await _budgetRepository.SaveChangesAsync();

            _logger.LogInformation(
                "Budget updated successfully | CategoryId:{CategoryId}, UserId:{UserId}",
                categoryId, userId);
        }

    }
}
