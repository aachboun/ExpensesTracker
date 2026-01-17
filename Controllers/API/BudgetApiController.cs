using ExpensesTracker.DTOs.Budget;
using ExpensesTracker.Models;
using ExpensesTracker.Services;
using ExpensesTracker.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace ExpensesTracker.Controllers.API
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BudgetApiController : ControllerBase
    {
        private readonly IBudgetService _budgetService;
        private readonly UserManager<ApplicationUser> _userManager;

        public BudgetApiController(IBudgetService budgetService, UserManager<ApplicationUser> userManager)
        {
            _budgetService = budgetService;
            _userManager = userManager;
        }
        [HttpPost("categories/{CategoryId:int}")]
        public async Task<IActionResult> CreateBudgetAsync([FromBody] CreateBudgetDto dto, int CategoryId)
        {
            var UserId = _userManager.GetUserId(User);
            var budgetId = await _budgetService.CreateAsync(dto, UserId, CategoryId);

            return CreatedAtAction(
                nameof(GetCurrent), new {id = CategoryId}, null);  
        }
        [HttpPost("global")]
        public async Task<IActionResult> CreateGlobale([FromBody] CreateBudgetDto dto)
        {
            var UserId = _userManager.GetUserId(User);
            int ? CategoryId = null;
            var budgetId = await _budgetService.CreateAsync(dto, UserId, CategoryId);

            return CreatedAtAction(
                nameof(GetCurrentGlobal), new { id = budgetId }) ;

        }
        [HttpGet("categories/{CategoryId:int}")]
        public async Task<IActionResult> GetCurrent(int CategoryId)
        {
            var UserId = _userManager.GetUserId(User);
            var budget = await _budgetService.GetByIdAsync(CategoryId, UserId);
            return Ok(budget);
        }
        [HttpGet("categories/{CategoryId:int}/period")]
        public async Task<IActionResult> GetByPeriode(int CategoryId , [FromQuery] DateOnly From, [FromQuery] DateOnly To)
        {
            var UserId = _userManager.GetUserId(User);
            var budget =await _budgetService.GetByPeriodeAsync(CategoryId,UserId,From, To);

            return Ok(budget);

        }
        [HttpGet("global")]
        public async Task<IActionResult> GetCurrentGlobal()
        {
            var UserId = _userManager.GetUserId(User);
            int? CategoryId = null;
            var budget = await _budgetService.GetByIdAsync(CategoryId,UserId);

            return Ok(budget);
        }
        [HttpGet("global/period")]
        public async Task<IActionResult> GetGlobalByPeriode([FromQuery]DateOnly From , [FromQuery]DateOnly To)
        {
            var UserId = _userManager.GetUserId(User);
            int ? CategoryId = null;
            var budget = await _budgetService.GetByPeriodeAsync(CategoryId,UserId, From, To);
            return Ok(budget);
        }
        [HttpDelete("categories/{CategoryId:int}")]
        public async Task<IActionResult> Delete(int CategoryId)
        {
            var UserId = _userManager.GetUserId(User);
              await _budgetService.Delete(CategoryId, UserId);
            return NoContent();
        }
        [HttpDelete("global")]
        public async  Task<IActionResult> DeleteGlobal()
        {
            var UserId = _userManager.GetUserId(User);
            int? CategoryId = null;
               await _budgetService.Delete(CategoryId, UserId);

            return NoContent();
        }
        [HttpPut("categories/{CategoryId:int}")]
        public async Task<IActionResult> UpdateBudget([FromBody] UpdateBudgetDto dto , int CategoryId)
        {
            var UserId = _userManager.GetUserId(User);
              await _budgetService.UpdateBudgetAsync(CategoryId, dto, UserId);
            return NoContent();
        }
        [HttpPut("global")]
        public async Task<IActionResult> UpdateGlobal([FromBody] UpdateBudgetDto dto)
        {
            var UserId = _userManager.GetUserId(User);
            int? CategoryId = null;
            await _budgetService.UpdateBudgetAsync(CategoryId, dto, UserId);
            return NoContent();
        }
    }
}
