using ExpensesTracker.DTOs.Expense;
using ExpensesTracker.Models;
using ExpensesTracker.Services.Interfaces;
using System.Runtime.CompilerServices;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.VisualBasic;
using ExpensesTracker.Repositories.Interfaces;


namespace ExpensesTracker.Services
{
    public class ExpenseService : IExpenseService
    {

        private readonly IExpenseRepository _expenseRepository;
        private readonly ILogger<ExpenseService> _logger;
        private readonly IMapper _mapper;

        public ExpenseService(IExpenseRepository expenseRepository, ILogger<ExpenseService> logger, IMapper mapper)
        {
            _expenseRepository = expenseRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<int> CreateAsync(CreateExpenseDto dto , string UserId, int CategoryId)
        {
            _logger.LogInformation("Creating Expense| Id : {Id} |UserId: {UserId}",  UserId);
            var expense = _mapper.Map<Expense>(dto);
            expense.UserId = UserId;
            expense.CategoryId = CategoryId;
           
             await _expenseRepository.AddAsync(expense);
            await _expenseRepository.SaveChangesAsync();

            _logger.LogInformation("Expense Created | Id : {Id} |UserId: {UserId}",expense.Id,  UserId);

            return expense.Id;
        }

      
        public async  Task<List<ReadExpenseDto>> GetAllExpenses(string UserId)
        {
            var expenses = await _expenseRepository.GetExpenses(UserId);

            return _mapper.Map<List<ReadExpenseDto>>(expenses);
        }
        public async  Task<ReadExpenseDto> GetExpense(int Id, string UserId)
        {
           var expense = await _expenseRepository.GetById(Id, UserId);
            if (expense == null)
            {
                _logger.LogWarning("Expense not found | Id: {Id} | UserId: {UserId}", Id, UserId);
                throw new KeyNotFoundException("Expense not found");
            }
            return _mapper.Map<ReadExpenseDto>(expense);    
        }
        public async Task<List<ReadExpenseDto>> GetByPeriod(string UserId, DateOnly From, DateOnly To)
        {
            var expenses = await _expenseRepository.GetByPeriod(UserId, From, To);
            return _mapper.Map<List<ReadExpenseDto>>(expenses);
        }

        public async Task<List<ReadExpenseDto>> GetByCatAndPeriod(int CategoryId, string UserId, DateOnly From, DateOnly To)
        {
            var expenses = await _expenseRepository.GetByCatAndPeriod(CategoryId,UserId, From, To);
            return _mapper.Map<List<ReadExpenseDto>>(expenses);
        }
        public async Task DeleteAsync(int Id , string UserId)
        {
            var expense = await _expenseRepository.GetById(Id, UserId);
            if (expense == null)
            {
                _logger.LogWarning("Delete Failed | Id: {Id} | UserId: {UserId}", Id, UserId);
                throw new KeyNotFoundException("Expense not found");
            }
            _expenseRepository.Delete(expense);
            await _expenseRepository.SaveChangesAsync();

            _logger.LogInformation("Expense Deleted | Id : {Id} |UserId: {UserId}", Id , UserId);
        }
        public async Task UpdateExpense( int Id ,UpdateExpenseDto dto , string UserId)
        {
            var expense = await _expenseRepository.GetById(Id , UserId);

            if (expense == null)
            {
                _logger.LogWarning("Update Failed | Id: {Id} | UserId: {UserId}", Id, UserId);
                throw new KeyNotFoundException("Expense not found");
            }

            _mapper.Map(dto, expense);
            await _expenseRepository.SaveChangesAsync();

            _logger.LogInformation("Expense Updated | Id : {Id} |UserId: {UserId}", Id, UserId);
        }
    }
}
