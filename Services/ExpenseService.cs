using ExpensesTracker.DTOs.Expense;
using ExpensesTracker.Models;
using ExpensesTracker.Repositories;
using ExpensesTracker.Services.Interfaces;
using System.Runtime.CompilerServices;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.VisualBasic;


namespace ExpensesTracker.Services
{
    public class ExpenseService : IExpense
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

        public async Task CreateAsync(CreateExpenseDto dto , string UserId)
        {
            var expense = _mapper.Map<Expense>(dto);
            expense.UserId = UserId;
           
             await _expenseRepository.AddAsync(expense);
            await _expenseRepository.SaveChangesAsync();   


        }

      
        public async  Task<List<ReadExpenseDto>> GetAllExpenses(string UserId)
        {
            var expenses = await _expenseRepository.GetExpenses(UserId);
            if (expenses == null)
            {
                return null;
            }

            return _mapper.Map<List<ReadExpenseDto>>(expenses);
        }
        public async  Task<ReadExpenseDto> GetExpense(int id, string UserId)
        {
           var expense = await _expenseRepository.GetById(id, UserId);
            if (expense == null)
            {
                return null;
            }
            return _mapper.Map<ReadExpenseDto>(expense);    
        }
        public async Task DeleteAsync(int id , string userId)
        {
            var expense = await _expenseRepository.GetById(id, userId);
            
            _expenseRepository.DeleteAsync(expense);
            await _expenseRepository.SaveChangesAsync();

        }
        public async Task UpdateExpense( int id ,UpdateExpenseDto dto , string UserId)
        {
            var expense = await _expenseRepository.GetById(id , UserId);
            _mapper.Map(dto, expense);
            await _expenseRepository.SaveChangesAsync();

        }
    }
}
