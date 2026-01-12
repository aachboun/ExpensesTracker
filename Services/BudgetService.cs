using AutoMapper;
using ExpensesTracker.DTOs.Budget;
using ExpensesTracker.Repositories;
using ExpensesTracker.Services.Interfaces;
using ExpensesTracker.Models;


namespace ExpensesTracker.Services
{
    public class BudgetService: IBudgetService
    {
        private readonly BudgetRepository _budgetRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;


        public BudgetService(BudgetRepository budgetRepository, IMapper mapper, ILogger logger)
        {
            _budgetRepository = budgetRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ReadBudgetDto> GetByIdAsync(int id , string UserId)
        {
            var budget = await _budgetRepository.GetGlobaleByIdAsync(id, UserId);
            if (budget == null) 
            {
              _logger.LogWarning("Budgte Not Found | Id:{id}, UserId:{UserId}",id , UserId);
                throw new KeyNotFoundException("Budget Not Found ");
                
            } 
          return _mapper.Map<ReadBudgetDto>(budget);
        }

        public async Task<int> CreateAsync(CreateBudgetDto dto, string UserId)
        {
            _logger.LogInformation("Creating  budget userid: {UserId}", UserId);
            var budget = _mapper.Map<Budget>(dto);
            budget.UserId = UserId;
            budget.EffectiveFrom = DateTime.UtcNow;
            await _budgetRepository.AddAsync(budget);
            await _budgetRepository.SaveChangesAsync();

            _logger.LogInformation("Budget Created Successfully | id: {Budget.Id}, UserId:{UserId}", budget.Id, UserId);

            return budget.Id;
        }
        public async Task Delete(int id , string UserId)
        {
            var budget = await _budgetRepository.GetGlobaleByIdAsync(id, UserId);  
            if(budget == null)
            {
                _logger.LogWarning(" Delete Failed | Budget Not found | id :{Id}, UserId : {UserId}", id, UserId);
                throw new KeyNotFoundException("Budget Not Found ");
            }
            _budgetRepository.Delete(budget);
            await _budgetRepository.SaveChangesAsync(); 

            _logger.LogInformation(" Budget deleted| id : {Id}, UserId:{UserId}",id, UserId);
        }

        public async Task UpdateBudgetAsync(int id, UpdateBudgetDto dto, string UserId)
        {
            var oldbudget = await _budgetRepository.GetGlobaleByIdAsync(id, UserId);
            if (oldbudget == null)
            {
                _logger.LogWarning("failed to find budget | id : {id}, UserId:{UserId}", id, UserId);
                throw new KeyNotFoundException("Budget Not found ");
            }
           
            
            var newbudget = oldbudget;
            newbudget.EffectiveFrom = DateTime.UtcNow;
            oldbudget.EffectiveTo = DateTime.UtcNow;
            _mapper.Map(dto,oldbudget);
            
            await _budgetRepository.AddAsync(newbudget);
            await _budgetRepository.SaveChangesAsync();


            _logger.LogInformation(" Budget Updated succesfully | id :{id},UserId:{UserId}", id, UserId);

        } 
    }
}
