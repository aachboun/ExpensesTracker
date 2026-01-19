



using ExpensesTracker.DTOs.Expense;
using ExpensesTracker.Models;
using ExpensesTracker.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ExpensesTracker.Controllers.API
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseApiController : ControllerBase
    {
        private readonly IExpenseService _expenseService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ExpenseApiController(IExpenseService expenseRepository, UserManager<ApplicationUser> userManager)
        {
            _expenseService = expenseRepository;
            _userManager = userManager;
        }

        [HttpPost("categories/{CategoryId:int}")]

        public async Task<IActionResult> CreateAsync( int CategoryId,[FromBody] CreateExpenseDto dto)
        {
            var UserId = _userManager.GetUserId(User);
            var expenseId =   await _expenseService.CreateAsync( dto,UserId , CategoryId);

            return CreatedAtAction(
                nameof(GetExpense), new {expenseId}, expenseId);
        }
        [HttpGet("{expenseId:int}")]
        public async Task<IActionResult> GetExpense(int expenseId)
        {
            var UserId =  _userManager.GetUserId(User);
            var expense = await _expenseService.GetExpense(expenseId, UserId);

            return Ok(expense);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllExpenses()
        {
            var UsreId = _userManager.GetUserId(User);

            var expenses = await _expenseService.GetAllExpenses(UsreId);

            return Ok(expenses);
        }
        [HttpGet("period")]
        public async Task<IActionResult> GetByPeriod([FromQuery] DateOnly From ,[FromQuery] DateOnly To)
        {
            var UserId = _userManager.GetUserId(User);
            var expenses =  await _expenseService.GetByPeriod(UserId, From,To);

            return Ok(expenses);
        }
        [HttpGet("categories/{CategoryId:int}/period")]
        public async Task<IActionResult> GetByCatAndPeriod(int CategoryId,[FromQuery] DateOnly From ,[FromQuery] DateOnly To)
        {
            var UserId = _userManager.GetUserId(User);
            var expenses =  await _expenseService.GetByCatAndPeriod( CategoryId,UserId, From, To);
            return Ok(expenses);
        }
        [HttpDelete("{expenseId:int}")]
        public async Task<IActionResult> Delete(int expenseId)
        {
            var UserId = _userManager.GetUserId(User);
               await _expenseService.DeleteAsync(expenseId,UserId);
            return NoContent();
        }
        [HttpPut("{expenseId:int}")]
        public async Task<IActionResult> UpdateExpense(int expenseId, [FromBody] UpdateExpenseDto dto)
        {
            var UserId = _userManager.GetUserId(User);
            await _expenseService.UpdateExpense(expenseId, dto,UserId);
            return NoContent();

        }
    }
}
