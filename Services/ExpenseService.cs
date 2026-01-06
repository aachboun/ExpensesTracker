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

        public async Task<int> CreateAsync(CreateExpenseDto dto , string UserId)
        {
            _logger.LogInformation("Creating Expense| Id : {Id} |UserId: {UserId}",  UserId);
            var expense = _mapper.Map<Expense>(dto);
            expense.UserId = UserId;
           
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
        public async  Task<ReadExpenseDto> GetExpense(int id, string UserId)
        {
           var expense = await _expenseRepository.GetById(id, UserId);
            if (expense == null)
            {
                _logger.LogWarning("Expense not found | Id: {Id} | UserId: {UserId}", id, UserId);
                throw new KeyNotFoundException("Expense not found");
            }
            return _mapper.Map<ReadExpenseDto>(expense);    
        }
        public async Task DeleteAsync(int id , string UserId)
        {
            var expense = await _expenseRepository.GetById(id, UserId);
            if (expense == null)
            {
                _logger.LogWarning("Delete Failed | Id: {Id} | UserId: {UserId}", id, UserId);
                throw new KeyNotFoundException("Expense not found");
            }
            _expenseRepository.DeleteAsync(expense);
            await _expenseRepository.SaveChangesAsync();

            _logger.LogInformation("Expense Deleted | Id : {Id} |UserId: {UserId}", id , UserId);
        }
        public async Task UpdateExpense( int id ,UpdateExpenseDto dto , string UserId)
        {
            var expense = await _expenseRepository.GetById(id , UserId);

            if (expense == null)
            {
                _logger.LogWarning("Update Failed | Id: {Id} | UserId: {UserId}", id, UserId);
                throw new KeyNotFoundException("Expense not found");
            }

            _mapper.Map(dto, expense);
            await _expenseRepository.SaveChangesAsync();

            _logger.LogInformation("Expense Updated | Id : {Id} |UserId: {UserId}", id, UserId);
        }
    }
}
